let currentEditId = null;

//Загрузка данных при старте
document.addEventListener('DOMContentLoaded', () => {
  fetchWasteTypes();
  fetchUsersForSelect();
  fetchAchievements();
  fetchUserStats();
  fetchCollectionPoints();
});

//Переключение вкладок
function showTab(tabId) {
  document.querySelectorAll('.tab-content').forEach(el => el.classList.remove('active'));
  document.getElementById(tabId).classList.add('active');

  document.querySelectorAll('.tab').forEach(el => el.classList.remove('active'));
  event.currentTarget.classList.add('active');
}

//типы отходовв
async function fetchWasteTypes() {
  const res = await fetch('/api/waste-types');
  const data = await res.json();

  const list = document.getElementById('wasteTypesList');
  list.innerHTML = '';

  data.forEach(type => {
    const li = document.createElement('li');
    li.setAttribute('data-id', type.id);
    li.innerHTML = `
      ${type.name} — ${type.eco_points} очков за кг
      <button onclick="editWasteType(${type.id})" style="margin-left: 10px;">Редактировать</button>
      <button onclick="deleteWasteType(${type.id})" style="margin-left: 5px; background-color: #e53935; color: white;">Удалить</button>
    `;
    list.appendChild(li);
  });
}

//обработчик формы добавления
document.getElementById('addWasteTypeForm')?.addEventListener('submit', async (e) => {
  e.preventDefault();

  const name = document.getElementById('name').value.trim();
  const ecoPoints = parseInt(document.getElementById('ecoPoints').value);

  if (!name || isNaN(ecoPoints)) {
    alert('Введите корректные данные');
    return;
  }

  const res = await fetch('/api/waste-types', {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ name, eco_points: ecoPoints })
  });

  if (res.ok) {
    document.getElementById('name').value = '';
    document.getElementById('ecoPoints').value = '';
    fetchWasteTypes();
  } else {
    alert('Ошибка при добавлении типа отходов');
  }
});

//редактирование отходов
function editWasteType(id) {
  const li = document.querySelector(`[data-id="${id}"]`);
  const [name, points] = li.textContent.split(' — ');

  document.getElementById('editId').value = id;
  document.getElementById('editName').value = name;
  document.getElementById('editEcoPoints').value = parseInt(points);
  document.getElementById('editWasteTypeForm').classList.remove('hidden');
  currentEditId = id;
}

function cancelEdit() {
  document.getElementById('editWasteTypeForm').classList.add('hidden');
  document.getElementById('editWasteTypeForm').reset();
  currentEditId = null;
}

document.getElementById('editWasteTypeForm')?.addEventListener('submit', async (e) => {
  e.preventDefault();

  const id = parseInt(document.getElementById('editId').value);
  const name = document.getElementById('editName').value.trim();
  const eco_points = parseInt(document.getElementById('editEcoPoints').value);

  if (!name || isNaN(eco_points)) {
    alert('Введите корректные данные');
    return;
  }

  const res = await fetch(`/api/waste-types/${id}`, {
    method: 'PUT',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ name, eco_points })
  });

  if (res.ok) {
    cancelEdit();
    fetchWasteTypes();
  } else {
    alert('Ошибка при редактировании');
  }
});

async function deleteWasteType(id) {
  if (!confirm('Вы уверены, что хотите удалить этот тип отхода?')) return;

  const res = await fetch(`/api/waste-types/${id}`, {
    method: 'DELETE'
  });

  if (res.ok) {
    fetchWasteTypes();
  } else {
    alert('Ошибка при удалении');
  }
}

//пункты приема
async function fetchCollectionPoints(query = '') {
  const url = query ? `/api/collection-points?search=${encodeURIComponent(query)}` : '/api/collection-points';
  const res = await fetch(url);
  const data = await res.json();

  const list = document.getElementById('collectionPointsList');
  list.innerHTML = '';

  data.forEach(point => {
    const li = document.createElement('li');
    li.textContent = `${point.name}, ${point.address}`;
    list.appendChild(li);
  });
}

document.getElementById('searchCollectionPointsForm')?.addEventListener('submit', async (e) => {
  e.preventDefault();
  const query = document.getElementById('searchAddress').value.trim();
  fetchCollectionPoints(query);
});

//пользователи
async function fetchUsersForSelect() {
  const res = await fetch('/api/users');
  const users = await res.json();

  const select = document.getElementById('reportUserId');
  select.innerHTML = '<option value="">Выберите пользователя</option>';

  users.forEach(user => {
    const opt = document.createElement('option');
    opt.value = user.id;
    opt.textContent = user.username;
    select.appendChild(opt);
  });
}

async function fetchWasteTypesForSelect() {
  const res = await fetch('/api/waste-types');
  const types = await res.json();

  const select = document.getElementById('reportWasteTypeId');
  select.innerHTML = '<option value="">Выберите тип отхода</option>';

  types.forEach(type => {
    const opt = document.createElement('option');
    opt.value = type.id;
    opt.textContent = `${type.name} (${type.eco_points} очков/кг)`;
    select.appendChild(opt);
  });
}

fetchWasteTypesForSelect();

//отчеты
document.getElementById('addReportForm')?.addEventListener('submit', async (e) => {
  e.preventDefault();

  const userId = parseInt(document.getElementById('reportUserId').value);
  const wasteTypeId = parseInt(document.getElementById('reportWasteTypeId').value);
  const weightKg = parseFloat(document.getElementById('reportWeightKg').value);

  if (!userId || !wasteTypeId || !weightKg || weightKg <= 0) {
    alert('Заполните все поля корректно');
    return;
  }

  const res = await fetch('/api/reports', {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ user_id: userId, waste_type_id: wasteTypeId, weight_kg: weightKg })
  });

  if (res.ok) {
    document.getElementById('reportWeightKg').value = '';
    fetchUserStats();
    alert('Отчет успешно добавлен');
  } else {
    alert('Ошибка при добавлении отчета');
  }
});

//статистика пользователей
async function fetchUserStats() {
  const res = await fetch('/api/users');
  const users = await res.json();

  const container = document.getElementById('userStatsContainer');
  container.innerHTML = '';

  for (const user of users) {
    const div = document.createElement('div');
    div.className = 'card';

    div.innerHTML = `
      <strong>${user.username}</strong><br/>
      Баллы: ${user.total_points}<br/>
      Отчеты: ${user.reports_count}
    `;

    const achRes = await fetch(`/api/users/${user.id}/achievements`);
    const achievements = await achRes.json();

    if (achievements.length > 0) {
      div.innerHTML += `<br/><strong>Достижения:</strong><ul>`;
      achievements.forEach(a => {
        div.innerHTML += `<li>${a.title}</li>`;
      });
      div.innerHTML += `</ul>`;
    }

    container.appendChild(div);
  }
}

//достижения
let currentEditAchievementId = null;

async function fetchAchievements() {
  const res = await fetch('/api/achievements');
  const data = await res.json();

  const list = document.getElementById('achievementsList');
  list.innerHTML = '';

  data.forEach(ach => {
    const li = document.createElement('li');
    li.setAttribute('data-id', ach.id);
    li.innerHTML = `
      <strong>${ach.title}</strong><br/>
      ${ach.description || ''}<br/>
      Требуется баллов: ${ach.required_points}
      <button onclick="editAchievement(${ach.id})" style="margin-left: 10px;">Редактировать</button>
      <button onclick="deleteAchievement(${ach.id})" style="margin-left: 5px; background-color: #e53935; color: white;">Удалить</button>
    `;
    list.appendChild(li);
  });
}

document.getElementById('addAchievementForm')?.addEventListener('submit', async (e) => {
  e.preventDefault();

  const title = document.getElementById('achievementTitle').value.trim();
  const description = document.getElementById('achievementDescription').value.trim();
  const required_points = parseInt(document.getElementById('achievementPoints').value);

  if (!title || isNaN(required_points)) {
    alert('Заполните обязательные поля');
    return;
  }

  const res = await fetch('/api/achievements', {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ title, description, required_points })
  });

  if (res.ok) {
    document.getElementById('achievementTitle').value = '';
    document.getElementById('achievementDescription').value = '';
    document.getElementById('achievementPoints').value = '';
    fetchAchievements();
  } else {
    alert('Ошибка при добавлении достижения');
  }
});

function editAchievement(id) {
  const li = document.querySelector(`#achievementsList li[data-id="${id}"]`);
  if (!li) return;

  const title = li.querySelector('strong').textContent;
  const descriptionNode = li.childNodes[2];
  const pointsNode = li.childNodes[4];

  const description = descriptionNode?.textContent.trim() || '';
  const required_points = parseInt(pointsNode.textContent.replace('Требуется баллов: ', ''));

  document.getElementById('editAchievementId').value = id;
  document.getElementById('editAchievementTitle').value = title;
  document.getElementById('editAchievementDescription').value = description;
  document.getElementById('editAchievementPoints').value = required_points;

  document.getElementById('editAchievementForm').classList.remove('hidden');
}

function cancelEditAchievement() {
  document.getElementById('editAchievementForm').classList.add('hidden');
  document.getElementById('editAchievementForm').reset();
  currentEditAchievementId = null;
}

document.getElementById('editAchievementForm')?.addEventListener('submit', async (e) => {
  e.preventDefault();

  const id = parseInt(document.getElementById('editAchievementId').value);
  const title = document.getElementById('editAchievementTitle').value.trim();
  const description = document.getElementById('editAchievementDescription').value.trim();
  const required_points = parseInt(document.getElementById('editAchievementPoints').value);

  if (!title || isNaN(required_points)) {
    alert('Введите корректные данные');
    return;
  }

  const res = await fetch(`/api/achievements/${id}`, {
    method: 'PUT',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ title, description, required_points })
  });

  if (res.ok) {
    cancelEditAchievement();
    fetchAchievements();
  } else {
    alert('Ошибка при редактировании');
  }
});

async function deleteAchievement(id) {
  if (!confirm('Вы уверены, что хотите удалить это достижение?')) return;

  const res = await fetch(`/api/achievements/${id}`, {
    method: 'DELETE'
  });

  if (res.ok) {
    fetchAchievements();
  } else {
    alert('Ошибка при удалении');
  }
}