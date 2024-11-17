#include <iostream>
#include <string>
#include <chrono>
#include <thread>

#ifdef ARDUINO
#include <WiFi.h>
#include <HTTPClient.h>
#include <ArduinoJson.h>
#else
#include <json/json.h>
#include <curl/curl.h>
#endif

class IoTClient {
private:
    std::string api_url;
    std::string api_key;
    std::string device_id;

    // WriteCallback used for Windows (cURL)
    static size_t WriteCallback(void* contents, size_t size, size_t nmemb, std::string* response) {
        size_t totalSize = size * nmemb;
        response->append((char*)contents, totalSize);
        return totalSize;
    }

    void displayStatus(const std::string& message) {
        std::cout << "[IoTClient] " << message << std::endl;
    }

public:
    IoTClient(const std::string& url, const std::string& key, const std::string& device)
        : api_url(url), api_key(key), device_id(device) {
        if (!api_url.empty() && api_url.back() == '/') {
            api_url.pop_back();
        }
    }

    bool initialize() {
#ifdef ARDUINO
        // For Arduino, no cURL initialization is needed
        displayStatus("Client initialized successfully (Arduino)");
        return true;
#else
        // For Windows, initialize cURL
        CURLcode res = curl_global_init(CURL_GLOBAL_DEFAULT);
        if (res != CURLE_OK) {
            displayStatus("Failed to initialize cURL");
            return false;
        }
        displayStatus("Client initialized successfully (Windows)");
        return true;
#endif
    }

    bool getTransactions() {
        displayStatus("Fetching transactions...");

#ifdef ARDUINO
        // Arduino-specific code using HTTPClient
        HTTPClient http;
        http.begin(api_url.c_str());  // Specify the URL
        http.addHeader("API-KEY", api_key.c_str());  // Specify the API key
        http.addHeader("Content-Type", "application/json");
        http.addHeader("Accept", "application/json");

        int httpCode = http.GET();  // Make the GET request

        if (httpCode > 0) {
            String payload = http.getString();
            processTransactions(payload);  // Process the response
            http.end();  // Free resources
            return true;
        }
        else {
            displayStatus("HTTP request failed");
            http.end();
            return false;
        }
#else
        // Windows-specific code using cURL
        CURL* curl = curl_easy_init();
        if (!curl) {
            displayStatus("Failed to initialize cURL handle");
            return false;
        }

        std::string url = api_url + "/references/transactions";
        std::string response;

        struct curl_slist* headers = nullptr;
        headers = curl_slist_append(headers, ("API-KEY: " + api_key).c_str());
        headers = curl_slist_append(headers, "Content-Type: application/json");
        headers = curl_slist_append(headers, "Accept: application/json");

        curl_easy_setopt(curl, CURLOPT_URL, url.c_str());
        curl_easy_setopt(curl, CURLOPT_HTTPHEADER, headers);
        curl_easy_setopt(curl, CURLOPT_WRITEFUNCTION, WriteCallback);
        curl_easy_setopt(curl, CURLOPT_WRITEDATA, &response);
        curl_easy_setopt(curl, CURLOPT_SSL_VERIFYPEER, 0L);
        curl_easy_setopt(curl, CURLOPT_SSL_VERIFYHOST, 0L);

        CURLcode res = curl_easy_perform(curl);

        if (res != CURLE_OK) {
            displayStatus("cURL request failed: " + std::string(curl_easy_strerror(res)));
            curl_slist_free_all(headers);
            curl_easy_cleanup(curl);
            return false;
        }

        long http_code = 0;
        curl_easy_getinfo(curl, CURLINFO_RESPONSE_CODE, &http_code);

        curl_slist_free_all(headers);
        curl_easy_cleanup(curl);

        if (http_code != 200) {
            displayStatus("HTTP error: " + std::to_string(http_code));
            return false;
        }

        processTransactions(response);  // Process the JSON response
        return true;
#endif
    }

private:
    void processTransactions(const std::string& response) {
#ifdef ARDUINO
        // Arduino-specific JSON parsing using ArduinoJson
        DynamicJsonDocument doc(1024);
        DeserializationError error = deserializeJson(doc, response);

        if (error) {
            displayStatus("Failed to parse JSON");
            return;
        }

        JsonArray transactions = doc["$values"].as<JsonArray>();
        displayTransactionStats(transactions);
#else
        // Windows-specific JSON parsing using jsoncpp
        Json::Value root;
        Json::Reader reader;

        if (reader.parse(response, root)) {
            const Json::Value transactions = root["$values"];
            if (transactions.isArray()) {
                displayTransactionStats(transactions);
            }
        }
        else {
            displayStatus("Failed to parse JSON response");
        }
#endif
    }

    void displayTransactionStats(const Json::Value& transactions) {
        double total_amount = 0;
        int high_count = 0;
        int med_count = 0;
        int low_count = 0;

        for (const Json::Value& transaction : transactions) {
            double amount = std::abs(transaction["transactionAmount"].asDouble());
            total_amount += amount;

            if (amount > 1000) high_count++;
            else if (amount >= 100) med_count++;
            else low_count++;
        }

        // Display results
        std::cout << "\nTransaction Analysis:" << std::endl;
        std::cout << "Total transactions: " << transactions.size() << std::endl;
        std::cout << "Total amount: " << total_amount << std::endl;
        std::cout << "\nAmount Distribution:" << std::endl;
        std::cout << "High (>1000): " << high_count << std::endl;
        std::cout << "Medium (100-1000): " << med_count << std::endl;
        std::cout << "Low (<100): " << low_count << std::endl;
    }
};

#ifdef ARDUINO
const char* ssid = "your-ssid";
const char* password = "your-password";
#endif

int main() {
    const std::string API_URL = "https://192.168.56.10:5074/api";
    const std::string API_KEY = "51e0ec98-ef1c-40be-b73c-7eb22de45a93";
    const std::string DEVICE_ID = "your-device-id";

    IoTClient client(API_URL, API_KEY, DEVICE_ID);

#ifdef ARDUINO
    // Arduino-specific initialization
    WiFi.begin(ssid, password);
    while (WiFi.status() != WL_CONNECTED) {
        delay(1000);
        Serial.println("Connecting to WiFi...");
    }
    Serial.println("Connected to WiFi");
#endif

    if (!client.initialize()) {
        std::cerr << "Failed to initialize client" << std::endl;
        return 1;
    }

    while (true) {
        if (!client.getTransactions()) {
            std::cerr << "Failed to fetch transactions" << std::endl;
        }

        // Platform-specific delay
#ifdef ARDUINO
        delay(60000);  // Arduino uses delay
#else
        std::this_thread::sleep_for(std::chrono::seconds(60));  // Windows uses std::this_thread::sleep_for
#endif
    }

    return 0;
}
