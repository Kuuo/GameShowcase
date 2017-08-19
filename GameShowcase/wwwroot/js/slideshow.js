var slideIndex = 1;
showSlides(slideIndex);

function plusSlides(n) {
    showSlides(slideIndex += n);
}

function currentSlide(n) {
    showSlides(slideIndex = n);
}

function showSlides(n) {
    var slides = document.getElementsByClassName("slideshow");
    var thumbs = document.getElementsByClassName("slideshow-thumb");

    if (n > slides.length) { slideIndex = 1 }
    if (n < 1) { slideIndex = slides.length }

    for (var i = 0; i < slides.length; i++) {
        slides[i].style.display = "none";
    }

    for (var i = 0; i < thumbs.length; i++) {
        thumbs[i].className = thumbs[i].className.replace(" slideshow-thumb-active", "");
    }

    slides[slideIndex - 1].style.display = "block";
    thumbs[slideIndex - 1].className += " slideshow-thumb-active";
}
