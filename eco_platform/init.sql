-- Типы отходов
CREATE TABLE IF NOT EXISTS waste_types (
    id SERIAL PRIMARY KEY,
    name VARCHAR(255) NOT NULL UNIQUE,
    eco_points INT NOT NULL DEFAULT 0
);

-- Пункты приема
CREATE TABLE IF NOT EXISTS collection_points (
    id SERIAL PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    address TEXT NOT NULL UNIQUE,  -- <-- Добавлено UNIQUE
    latitude FLOAT,
    longitude FLOAT
);

-- Пользователи
CREATE TABLE IF NOT EXISTS users (
    id SERIAL PRIMARY KEY,
    username VARCHAR(255) NOT NULL UNIQUE,
    total_points INT DEFAULT 0
);

-- Отчеты об утилизации
CREATE TABLE IF NOT EXISTS reports (
    id SERIAL PRIMARY KEY,
    user_id INT REFERENCES users(id),
    waste_type_id INT REFERENCES waste_types(id),
    weight_kg NUMERIC NOT NULL,
    points_earned INT,
    timestamp TIMESTAMP DEFAULT NOW()
);

-- Достижения
CREATE TABLE IF NOT EXISTS achievements (
    id SERIAL PRIMARY KEY,
    title VARCHAR(255) NOT NULL UNIQUE,  -- <-- добавлено UNIQUE
    description TEXT,
    required_points INT NOT NULL
);

-- Привязка пользователей к достижениям
CREATE TABLE IF NOT EXISTS user_achievements (
    user_id INT REFERENCES users(id),
    achievement_id INT REFERENCES achievements(id),
    PRIMARY KEY (user_id, achievement_id)
);

-- Заполнение типов отходов
INSERT INTO waste_types (name, eco_points) VALUES
('Пластик', 10),
('Бумага', 5),
('Стекло', 8),
('Металл', 12),
('Органические отходы', 3)
ON CONFLICT (name) DO NOTHING;

-- Заполнение пунктов приема
INSERT INTO collection_points (name, address, latitude, longitude) VALUES
('Эко-пункт Центральный', 'ул. Ленина, д. 10', 55.7558, 37.6173),
('Эко-пункт Западный', 'ул. Победы, д. 25', 55.7489, 37.5872),
('Эко-пункт Восточный', 'пр-т Мира, д. 50', 55.7621, 37.6478)
ON CONFLICT (address) DO NOTHING;

-- Заполнение пользователей
INSERT INTO users (username, total_points) VALUES
('ivanov', 150),
('petrov', 80),
('smirnova', 200),
('test_user', 0)
ON CONFLICT (username) DO NOTHING;

-- Заполнение достижений
INSERT INTO achievements (title, description, required_points) VALUES
('Начинающий эколог', 'Собрано 50 экопунктов', 50),
('Зеленый лидер', 'Собрано 200 экопунктов', 200),
('Чистый город', 'Собрано 500 экопунктов', 500),
('Герой планеты', 'Собрано 1000 экопунктов', 1000)
ON CONFLICT (title) DO NOTHING;

-- Заполнение отчетов об утилизации
INSERT INTO reports (user_id, waste_type_id, weight_kg, points_earned) VALUES
(1, 1, 3.5, 35),   -- Иванов, пластик
(1, 2, 2.0, 10),
(2, 3, 5.0, 40),
(3, 4, 2.5, 30),
(3, 5, 10.0, 30)
ON CONFLICT (id) DO NOTHING;

-- Заполнение связи пользователей с достижениями
INSERT INTO user_achievements (user_id, achievement_id) VALUES
(1, 1),
(3, 1),
(3, 2);