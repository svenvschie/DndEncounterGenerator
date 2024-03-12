var currentIndex = 0;

function selectNextItemInList() {
    
    var items = document.querySelectorAll('.List p');

    if (items.length === 0) {
        alert('Please generate an encounter first.');
        return;
    }

    items.forEach(function (item) {
        item.classList.remove('highlight');
    });

    if (currentIndex < items.length) {
        items[currentIndex].classList.add('highlight');
    }

    currentIndex = (currentIndex + 1) % items.length;
}