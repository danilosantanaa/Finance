const THEME = {
    auto: 'auto',
    dark: 'dark',
    light: 'light'
}

function changeTheme(theme) {
    setTheme(theme)
    assigmentTheme(theme)
}

function bindTheme() {
    let theme = getThemeLocalStorage()

    if (theme == null) {
        assigmentTheme(THEME.auto)
        theme = getThemeLocalStorage()
    }

    setTheme(theme)

    return theme
}

function setTheme(theme) {
    const match = window.matchMedia('(prefers-color-scheme: dark)').matches;
    const el_html = document.querySelector('html');

    if (el_html.hasAttribute(THEME.dark)) {
        el_html.removeAttribute(THEME.dark)
    }

    if (el_html.hasAttribute(THEME.light)) {
        el_html.removeAttribute(THEME.light)
    }

    if (THEME[theme] == THEME.auto) {
        el_html.setAttribute(match ? THEME.dark : THEME.light, "")
    } else {
        el_html.setAttribute(THEME[theme], "")
    }

    assigmentTheme(theme)
}

function assigmentTheme(theme) {
    try {
        localStorage.setItem('theme', theme);
    } catch (e) {
        console.error(e)
    }
}

function getThemeLocalStorage() {
    return localStorage.getItem('theme')
}