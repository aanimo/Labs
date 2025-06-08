let books = [];
let currentPage = 1;
const booksPerPage = 6;

async function searchBooks() {
    const query = document.getElementById('searchInput').value.trim();
    const resultsDiv = document.getElementById('results');

    if (!query) {
        alert("Введите ключевое слово для поиска");
        return;
    }

    resultsDiv.innerHTML = '<p>Загрузка...</p>';

    try {
        const response = await fetch(`/search?q=${encodeURIComponent(query)}`);
        books = await response.json();

        currentPage = 1;
        renderBooks();
    } catch (err) {
        resultsDiv.innerHTML = '<p>Ошибка загрузки данных.</p>';
        console.error(err);
    }
}

function renderBooks() {
    const resultsDiv = document.getElementById('results');
    const start = (currentPage - 1) * booksPerPage;
    const end = start + booksPerPage;
    const pageBooks = books.slice(start, end);

    resultsDiv.innerHTML = '';

    if (pageBooks.length === 0) {
        resultsDiv.innerHTML = '<p>Книги не найдены.</p>';
        return;
    }

    pageBooks.forEach(book => {
        const card = document.createElement('div');
        card.className = 'card';

        card.innerHTML = `
            <img src="${book.cover}" alt="Обложка">
            <div class="info">
                <h3>${book.title}</h3>
                <p><strong>Автор:</strong> ${book.authors.join(', ')}</p>
                <p><strong>Год:</strong> ${book.publishedDate}</p>
                <p class="description">${book.description}</p>
                <a href="${book.previewLink}" target="_blank">Подробнее</a>
                <div class="source-badge ${book.source === 'google' ? 'google-source' : 'openlibrary-source'}">
                    Источник: ${book.source === 'google' ? 'Google Books' : 'Open Library'}
                </div>
            </div>
        `;
        resultsDiv.appendChild(card);
    });

    document.getElementById('prevBtn').disabled = currentPage === 1;
    document.getElementById('nextBtn').disabled = end >= books.length;
    document.getElementById('pageInfo').textContent = `Страница ${currentPage}`;
}

function nextPage() {
    if ((currentPage - 1) * booksPerPage + booksPerPage < books.length) {
        currentPage++;
        renderBooks();
    }
}

function prevPage() {
    if (currentPage > 1) {
        currentPage--;
        renderBooks();
    }
}