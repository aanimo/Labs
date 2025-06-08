// routes/userAchievementRoutes.js
const express = require('express');
const router = express.Router();
const userAchievementController = require('../controllers/userAchievementController');

router.get('/users/:userId/achievements', userAchievementController.getUserAchievements);
router.post('/users/:userId/achievements', userAchievementController.addUserAchievement);

module.exports = router;
