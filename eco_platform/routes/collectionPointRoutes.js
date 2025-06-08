// routes/collectionPointRoutes.js
const express = require('express');
const router = express.Router();
const collectionPointController = require('../controllers/collectionPointController');

router.get('/collection-points', collectionPointController.getCollectionPoints);
router.get('/collection-points/:id', collectionPointController.getCollectionPointById);
router.post('/collection-points', collectionPointController.createCollectionPoint);
router.put('/collection-points/:id', collectionPointController.updateCollectionPoint);
router.delete('/collection-points/:id', collectionPointController.deleteCollectionPoint);

module.exports = router;