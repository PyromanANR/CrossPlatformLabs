import platform
import requests
import json
import time
from datetime import datetime

# Raspberry Pi-specific imports (conditionally imported for Linux)
if platform.system() == "Linux":
    import RPi.GPIO as GPIO
    from RPLCD.i2c import CharLCD  
    import board
    import digitalio
    import adafruit_ssd1306

class IoTClient:
    def __init__(self, api_url, device_id, api_key):
        """
        Initialize IoT client with API credentials and hardware support
        """
        self.api_url = api_url
        self.device_id = device_id
        self.api_key = api_key
        self.headers = {
            'API-KEY': self.api_key,
            'Content-Type': 'application/json'
        }

        # Check OS platform
        self.is_linux = platform.system() == "Linux"
        
        # Initialize GPIO and displays for Linux
        if self.is_linux:
            GPIO.setmode(GPIO.BCM)
            self.led_success = 18
            self.led_error = 23
            GPIO.setup(self.led_success, GPIO.OUT)
            GPIO.setup(self.led_error, GPIO.OUT)

            try:
                self.lcd = CharLCD('PCF8574', 0x27)
                self.has_lcd = True
            except:
                print("LCD not found, continuing without display")
                self.has_lcd = False

            try:
                i2c = board.I2C()
                self.oled = adafruit_ssd1306.SSD1306_I2C(128, 64, i2c)
                self.has_oled = True
            except:
                print("OLED not found, continuing without display")
                self.has_oled = False
        else:
            self.has_lcd = False
            self.has_oled = False

    def display_status(self, message):
        """Display status on console, LCD, or OLED"""
        print(message)  
        
        if self.is_linux:
            if self.has_lcd:
                self.lcd.clear()
                self.lcd.write_string(message[:32])
            if self.has_oled:
                self.oled.fill(0)
                self.oled.text(message, 0, 0, 1)
                self.oled.show()

    def indicate_status(self, success=True):
        """Indicate status with LEDs if on Linux"""
        if self.is_linux:
            if success:
                GPIO.output(self.led_success, GPIO.HIGH)
                GPIO.output(self.led_error, GPIO.LOW)
                time.sleep(1)
                GPIO.output(self.led_success, GPIO.LOW)
            else:
                GPIO.output(self.led_error, GPIO.HIGH)
                GPIO.output(self.led_success, GPIO.LOW)
                time.sleep(1)
                GPIO.output(self.led_error, GPIO.LOW)

    def get_transactions(self):
        """Retrieve transactions from API and provide detailed analysis"""
        try:
            self.display_status("Fetching data...")
            response = requests.get(
                f"{self.api_url}/references/transactions",
                headers=self.headers,
                verify=False
            )
        
            if response.status_code == 200:
                data = response.json()
                transactions = data.get('$values', [])
            
                self.indicate_status(True)
                self.display_status(f"Got {len(transactions)} transactions")
            
                print("\nRequest Successful!")
                print(f"Number of transactions received: {len(transactions)}")
            
                # Calculate total transaction amount
                total_amount = sum(float(t.get('transactionAmount', 0)) for t in transactions)
                print(f"Total transaction amount: {total_amount:.2f}")
          
            
                # Sort transactions by amount
                sorted_transactions = sorted(
                    transactions, 
                    key=lambda x: abs(float(x.get('transactionAmount', 0))), 
                    reverse=True
                )
            
                print("\nTop 5 Transactions by Amount:")
                for idx, transaction in enumerate(sorted_transactions[:5], 1):
                    amount = float(transaction.get('transactionAmount', 0))
                    print(f"{idx}. Amount: {amount:.2f}, ID: {transaction.get('transactionId')}")
            
                # Group transactions by amount range
                ranges = {
                    'High (>1000)': 0,
                    'Medium (100-1000)': 0,
                    'Low (<100)': 0
                }
            
                for transaction in transactions:
                    amount = abs(float(transaction.get('transactionAmount', 0)))
                    if amount > 1000:
                        ranges['High (>1000)'] += 1
                    elif amount >= 100:
                        ranges['Medium (100-1000)'] += 1
                    else:
                        ranges['Low (<100)'] += 1
            
                print("\nTransaction Distribution by Amount:")
                for range_name, count in ranges.items():
                    print(f"{range_name}: {count} transactions")
            
                # Group transactions by type
                type_groups = {}
                for transaction in transactions:
                    trans_type = transaction.get('transactionTypeCode', 'Unknown')
                    if trans_type not in type_groups:
                        type_groups[trans_type] = 0
                    type_groups[trans_type] += 1
            
                print("\nTransactions by Type:")
                for trans_type, count in type_groups.items():
                    print(f"{trans_type}: {count} transactions")
            
                return transactions
            else:
                self.indicate_status(False)
                self.display_status("API Error")
                print("\nRequest Failed!")
                print(f"Error Status Code: {response.status_code}")
                print(f"Error Response: {response.text}")
                return None
            
        except Exception as e:
            self.indicate_status(False)
            self.display_status("Error occurred")
            print(f"Error: {str(e)}")
            return None


    def cleanup(self):
        """Cleanup GPIO and displays if on Linux"""
        if self.is_linux:
            GPIO.cleanup()
            if self.has_lcd:
                self.lcd.clear()
            if self.has_oled:
                self.oled.fill(0)
                self.oled.show()

if __name__ == "__main__":
    API_URL = "https://192.168.56.10:5074/api"
    DEVICE_ID = "your-device-id"
    API_KEY = "51e0ec98-ef1c-40be-b73c-7eb22de45a93"
    
    client = IoTClient(API_URL, DEVICE_ID, API_KEY)
    
    try:
        while True:
            transactions = client.get_transactions()
            time.sleep(60)  # Check every minute
    except KeyboardInterrupt:
        print("\nStopping client...")
        client.cleanup()
