function query(name) {
  const url = window.location.href;
  const regex = new RegExp(`[?&]${name.replace(/[[\]]/g, '\\$&')}(=([^&#]*)|&|#|$)`);
  const results = regex.exec(url);
  if (!results) return null;
  if (!results[2]) return '';
  return decodeURIComponent(results[2].replace(/\+/g, ' '));
}

function createResult(result) {
  const row = document.createElement('div');
  row.className = 'border table-row';

  const content = document.createElement('div');
  content.className = 'table-row__content';
  row.appendChild(content);

  const rank = document.createElement('span');
  rank.innerHTML = `#${result.rank}`;
  content.appendChild(rank);

  const name = document.createElement('span');
  name.innerHTML = result.name;
  content.appendChild(name);

  const time = document.createElement('span');
  time.innerHTML = `${result.time} seconds`;
  content.appendChild(time);

  return row;
}

window.addEventListener('load', () => {
  const socket = io();

  const board = query('board');
  const page = query('page');

  const prevButton = document.getElementById('prevPage');
  const nextButton = document.getElementById('nextPage');

  socket.on('update', (response) => {
    const { results, pages } = response;
    const DOMResults = document.getElementById('results');
    DOMResults.innerHTML = '';

    results.forEach((result) => {
      DOMResults.appendChild(createResult(result));
    });

    if (page < pages) {
      nextButton.style.display = 'block';
    } else {
      nextButton.style.display = 'none';
    }
  });

  if (page > 1) {
    prevButton.style.display = 'block';
  } else {
    prevButton.style.display = 'none';
  }

  prevButton.addEventListener('click', () => {
    window.location.href = `/?board=${board}&page=${parseInt(page, 10) - 1}`;
  });
  nextButton.addEventListener('click', () => {
    window.location.href = `/?board=${board}&page=${parseInt(page, 10) + 1}`;
  });

  socket.emit('board', board, page);
});
