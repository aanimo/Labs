const express = require('express');
const app = express();
const PORT = 3000;

app.use(express.json());
app.use(express.static('public'));

const wasteTypeRoutes = require('./routes/wasteTypeRoutes');
const collectionPointRoutes = require('./routes/collectionPointRoutes');app.use('/api', wasteTypeRoutes);
const reportRoutes = require('./routes/reportRoutes');
const achievementRoutes = require('./routes/achievementRoutes');
const userRoutes = require('./routes/userRoutes');
const userAchievementRoutes = require('./routes/userAchievementRoutes');

app.use('/api', wasteTypeRoutes);
app.use('/api', collectionPointRoutes);
app.use('/api', reportRoutes);
app.use('/api', achievementRoutes);
app.use('/api', userRoutes);
app.use('/api', userAchievementRoutes);

app.listen(PORT, () => {
  console.log(`Сервер запущен на http://localhost:${PORT}`);
});