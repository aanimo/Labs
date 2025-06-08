const { Client } = require('pg');

const client = new Client({
    user: process.env.POSTGRES_USER || 'bahek',
    host: process.env.POSTGRES_HOST || 'localhost',
    database: process.env.POSTGRES_DB || 'eco_platform',
    password: process.env.POSTGRES_PASSWORD || 'pass123',
    port: process.env.POSTGRES_PORT || 5432,
});

client.connect();

module.exports = client;
