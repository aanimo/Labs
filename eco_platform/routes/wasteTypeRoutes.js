const express = require('express');
const router = express.Router();
const wasteTypeController = require('../controllers/wasteTypeController');

router.get('/waste-types', wasteTypeController.getWasteTypes);
router.get('/waste-types/:id', wasteTypeController.getWasteTypeById);
router.post('/waste-types', wasteTypeController.createWasteType);
router.put('/waste-types/:id', wasteTypeController.updateWasteType);
router.delete('/waste-types/:id', wasteTypeController.deleteWasteType);

module.exports = router;