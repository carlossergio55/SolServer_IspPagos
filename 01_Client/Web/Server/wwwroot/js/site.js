window.scrollToTop = function () {
    window.scrollTo(0, 0);
};
window.scrollToElement = function (elementId) {
    var element = document.getElementById(elementId);
    if (element) {
        element.scrollIntoView({ behavior: 'smooth', block: 'nearest' });
    }
};
window.scrollTimelineToRight = function (elementId) {
    const container = document.getElementById(elementId);
    if (container) {
        // Lleva el scroll horizontal al extremo derecho
        container.scrollLeft = container.scrollWidth;
    }
}

function setPositionForDialog(x, y) {
    var dialog = document.querySelector('.e-dialog');
    if (dialog) {
        // Hacer el di�logo invisible inicialmente
        dialog.style.opacity = 0;

        var rect = dialog.getBoundingClientRect();
        var winWidth = window.innerWidth;
        var winHeight = window.innerHeight;

        var left = x + rect.width > winWidth ? winWidth - rect.width : x;
        var top = y + rect.height > winHeight ? winHeight - rect.height : y;

        dialog.style.position = 'fixed';
        dialog.style.left = left + 'px';
        dialog.style.top = top + 'px';

        // Hacer el di�logo visible despu�s de ajustar la posici�n
        setTimeout(() => dialog.style.opacity = 1, 0);
    }
}



