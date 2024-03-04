const seccionComments = document.querySelector('.seccion_comments')
const comentar = document.querySelector('.comentar');

comentar.addEventListener('click', () => {


    if (seccionComments.classList.contains('none')) {
        seccionComments.classList.remove('none')
    }
    else {
        seccionComments.classList.add('none')
    }

})

