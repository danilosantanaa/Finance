const TYPEICON = {
    0: "question",
    1: "info",
    2: "success",
    3: "warning",
    4: "error"
}

const TITLE_DEFAULT = {
    0: "Ajuda",
    1: "Informações",
    2: "Sucesso",
    3: "Aviso",
    4: "Erro"
}

/**
 * 
 * @param {string} title
 * @param {Number} type
 * @param {Number} timer
 */
function ToastAlert(title, type, timer = 2000) {
    swal.fire({
        toast: true,
        icon: TYPEICON[type],
        title,
        position: 'top-right',
        showConfirmButton: false,
        timer,
        timerProgressBar: true,
        didOpen: (toast) => {
            toast.addEventListener('mouseenter', Swal.stopTimer)
            toast.addEventListener('mouseleave', Swal.resumeTimer)
        }
    })
}

/**
 * 
 * @param {Number} type
 * @param {string} text
 * @param {string} title
 */
function Alert(type, text, title = null) {
    swal.fire({
        icon: TYPEICON[type],
        title: ( title == null ? TITLE_DEFAULT[type] : title ),
        html: text
    })
}

/**
 * 
 * @param {string} title
 * @param {string} content
 * @param {string} textConfirm
 * @param {string} textCancel
 * @returns {boolean}
 */
function AlertConfirmation(content, title = "Aviso", textConfirm = "Sim", textCancel = "Não") {
    const WARNING = 3
   
    const confirmed = new Promise((resolve, reject) => {
        Swal.fire({
            title,
            html: content,
            icon: TYPEICON[WARNING],
            showCancelButton: true,
            confirmButtonColor: '#91c83e',
            cancelButtonColor: '#c1c1c1',
            confirmButtonText: textConfirm,
            cancelButtonText: textCancel
        }).then((result) => {

            if (result.isConfirmed) {
                resolve(true)
            }
            resolve(false)

        }).catch((e) => {
            console.error(e)
            reject(false)
        })

    })
  
    return confirmed
}