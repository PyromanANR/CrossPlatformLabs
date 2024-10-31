const axios = require('axios');
const https = require('https');
const logger = require('../config/logger');

const API_BASE_URL = 'https://192.168.56.10:5074/api';

// Create an Axios instance with a custom HTTPS agent that allows self-signed certificates
const axiosInstance = axios.create({
    baseURL: API_BASE_URL,
    httpsAgent: new https.Agent({
        rejectUnauthorized: false, // Disable certificate validation
    }),
});

describe('Accounts API Tests', () => {
    beforeAll(() => {
        logger.info('Starting Accounts API tests');
    });

    afterAll(() => {
        logger.info('Finished Accounts API tests');
    });

    test('GET /api/accounts should return all accounts', async () => {
        try {
            logger.info('Testing GET /api/accounts endpoint');
            const response = await axiosInstance.get('/accounts');

            logger.info(`Received ${JSON.stringify(response.data)} accounts`);
            expect(response.status).toBe(200);
            expect(Array.isArray(response.data.$values)).toBe(true);
        } catch (error) {
            logger.error(`Error testing GET /api/accounts: ${error.message}`);
            throw error;
        }
    });

    test('GET /api/accounts/{id} should return specific account', async () => {
        const accountId = 1;
        try {
            logger.info(`Testing GET /api/accounts/${accountId} endpoint`);
            const response = await axiosInstance.get(`/accounts/${accountId}`);

            logger.info(`Successfully retrieved account ${accountId}`);
            expect(response.status).toBe(200);
            expect(response.data).toHaveProperty('accountNumber');
        } catch (error) {
            logger.error(`Error testing GET /api/accounts/${accountId}: ${error.message}`);
            throw error;
        }
    });
});


describe('References API Tests', () => {
    beforeAll(() => {
        logger.info('Starting References API tests');
    });

    afterAll(() => {
        logger.info('Finished References API tests');
    });

    test('GET /api/references/customers should return all customers', async () => {
        try {
            logger.info('Testing GET /api/references/customers endpoint');
            const response = await axiosInstance.get('/references/customers');

            logger.info(`Received ${JSON.stringify(response.data)} customers`);
            expect(response.status).toBe(200);
            expect(Array.isArray(response.data.$values)).toBe(true);
        } catch (error) {
            logger.error(`Error testing GET /api/references/customers: ${error.message}`);
            throw error;
        }
    });
});


describe('Database Integration Tests', () => {
    beforeAll(() => {
        logger.info('Starting Database Integration tests');
    });

    afterAll(() => {
        logger.info('Finished Database Integration tests');
    });

    // SQL Server tests
    describe('SQL Server', () => {
        test('should perform basic operations with SQL Server', async () => {
            try {
                logger.info('Testing SQL Server connection');
                const response = await axiosInstance.get('/accounts');
                logger.info('Successfully connected to SQL Server');
                expect(response.status).toBe(200);
            } catch (error) {
                logger.error(`SQL Server test failed: ${error.message}`);
                throw error;
            }
        });
    });
});