![image](https://github.com/user-attachments/assets/759fed3f-5afe-4034-99e3-f37a1322b080)

# Інструкція з налаштування IoT-системи на Raspberry Pi

## Необхідне обладнання
- **Raspberry Pi** (3 або 4)
- **LCD дисплей I2C** (16x2 або 20x4) – опціонально
- **OLED дисплей I2C** (128x64) – опціонально
- **2 світлодіоди** (зелений і червоний)
- **2 резистори** (220 Ом)
- **Макетна плата**
- **З'єднувальні дроти**

---

## Схема підключення

### LED
- **Зелений LED**: GPIO 18 через резистор 220 Ом на GND
- **Червоний LED**: GPIO 23 через резистор 220 Ом на GND

### LCD (I2C)
- **SDA** → GPIO2 (SDA)
- **SCL** → GPIO3 (SCL)
- **VCC** → 5V
- **GND** → GND

### OLED (I2C)
- **SDA** → GPIO2 (SDA)
- **SCL** → GPIO3 (SCL)
- **VCC** → 3.3V
- **GND** → GND

---

## Підготовка Raspberry Pi

### Оновлення системи
```bash
sudo apt-get update
sudo apt-get upgrade
```

### Встановлення необхідних пакетів
```bash
sudo apt-get install python3-pip
sudo apt-get install python3-smbus
sudo apt-get install i2c-tools
```

### Активація I2C інтерфейсу
1. Запустіть конфігуратор:
   ```bash
   sudo raspi-config
   ```
2. Перейдіть до: **Interface Options** → **I2C** → **Enable**.

### Встановлення Python бібліотек
```bash
pip3 install requests
pip3 install RPLCD
pip3 install adafruit-circuitpython-ssd1306
pip3 install RPi.GPIO
```

---

## Перевірка I2C пристроїв
Використовуйте команду:
```bash
sudo i2cdetect -y 1
```
Це покаже адреси підключених I2C пристроїв.

---

## Запуск програми
Запустіть Python-скрипт:
```bash
python3 iot_client.py
```

---

## Функціональність
- **Отримання даних з API**
- **Відображення статусу** на LCD/OLED дисплеї
- **Індикація статусу** світлодіодами:
  - **Зелений**: Успішне отримання даних
  - **Червоний**: Помилка
- **Вивід детальної інформації** в термінал
- **Автоматичне оновлення** кожну хвилину

---

## Додаткові налаштування
- **Інтервал оновлення**:
  - Для зміни частоти оновлення змініть значення `time.sleep(60)` у коді.
- **Формат виводу на дисплей**:
  - Налаштуйте метод `display_status`.
- **Додавання нових LED індикаторів**:
  - Вкажіть нові GPIO піни для додаткових світлодіодів.
