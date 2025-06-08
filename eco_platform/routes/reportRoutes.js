const express = require('express');
const router = express.Router();
const reportController = require('../controllers/reportController');

router.get('/reports', reportController.getReports);
router.get('/reports/:id', reportController.getReportById);
router.post('/reports', reportController.createReport);
router.delete('/reports/:id', reportController.deleteReport);

module.exports = router;