const express = require('express');
const https = require('https');
const app = express();
const PORT = 3000;

app.use(express.static('public'));

// --- Вспомогательная функция ---
function fetchData(url, callback) {
    https.get(url, (res) => {
        let data = '';
        res.on('data', chunk => data += chunk);
        res.on('end', () => {
            try {
                callback(null, JSON.parse(data));
            } catch (e) {
                callback(e, null);
            }
        });
    }).on('error', err => callback(err, null));
}

// --- Google Books API ---
function fetchFromGoogleBooks(query, callback) {
    const url = `https://www.googleapis.com/books/v1/volumes?q= ${encodeURIComponent(query)}&maxResults=30`;
    fetchData(url, (err, data) => {
        if (err || !data || !data.items) {
            return callback(err || new Error('No results from Google Books'), null);
        }

        const books = data.items.map(item => {
            const info = item.volumeInfo;
            return {
                source: 'google',
                title: info.title,
                authors: info.authors || ['Неизвестный автор'],
                description: info.description || 'Описание отсутствует',
                publishedDate: info.publishedDate || 'Не указано',
                previewLink: info.previewLink || '#',
                cover: info.imageLinks?.thumbnail || '',
            };
        });

        callback(null, books);
    });
}

// --- Open Library API ---
function fetchFromOpenLibrary(query, callback) {
    const url = `https://openlibrary.org/search.json?q= ${encodeURIComponent(query)}&limit=30`;
    fetchData(url, (err, data) => {
        if (err || !data || !data.docs) {
            return callback(err || new Error('No results from Open Library'), null);
        }

        const books = data.docs.map(book => ({
            source: 'openlibrary',
            title: book.title,
            authors: book.author_name ? [...new Set(book.author_name)] : ['Неизвестный автор'],
            description: 'Нет описания',
            publishedDate: book.publish_date ? book.publish_date[0] : 'Не указано',
            previewLink: `https://openlibrary.org/works/ ${book.key}`,
            cover: book.cover_i ? `https://covers.openlibrary.org/b/id/ ${book.cover_i}-M.jpg` : '',
        }));

        callback(null, books);
    });
}

// --- Маршрут поиска книг ---
app.get('/search', (req, res) => {
    const query = req.query.q;
    if (!query) return res.status(400).json({ error: 'Введите ключевое слово для поиска' });

    // Сначала пробуем Google Books
    fetchFromGoogleBooks(query, (err, googleBooks) => {
        if (!err && googleBooks && googleBooks.length > 0) {
            res.header("Access-Control-Allow-Origin", "*");
            return res.json(googleBooks);
        }

        // Если Google Books не работает — используем Open Library
        console.warn('Falling back to Open Library due to Google Books failure');
        fetchFromOpenLibrary(query, (olErr, openLibraryBooks) => {
            if (!olErr && openLibraryBooks && openLibraryBooks.length > 0) {
                res.header("Access-Control-Allow-Origin", "*");
                return res.json(openLibraryBooks);
            }

            return res.status(500).json({ error: 'Не удалось получить данные ни от одного источника' });
        });
    });
});

app.listen(PORT, () => {
    console.log(`Сервер запущен на http://localhost:${PORT}`);
});