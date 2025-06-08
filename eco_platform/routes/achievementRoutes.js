// routes/achievementRoutes.js
const express = require('express');
const router = express.Router();
const achievementController = require('../controllers/achievementController');

router.get('/achievements', achievementController.getAchievements);
router.get('/achievements/:id', achievementController.getAchievementById);
router.post('/achievements', achievementController.createAchievement);
router.put('/achievements/:id', achievementController.updateAchievement);
router.delete('/achievements/:id', achievementController.deleteAchievement);

module.exports = router;