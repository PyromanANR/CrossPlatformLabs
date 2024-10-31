// jest.setup.js
const fs = require('fs');
const path = require('path');
const logger = require('./config/logger');

// Створюємо директорію для логів
const logsDir = path.join(__dirname, 'logs');
if (!fs.existsSync(logsDir)) {
    fs.mkdirSync(logsDir);
}

// Хуки для логування тестів
beforeAll(() => {
    logger.info('Starting test suite execution');
});

afterAll(() => {
    logger.info('Finished test suite execution');
});

