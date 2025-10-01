window.setSessionData = (key, value) => {
    sessionStorage.setItem(key, value);
};

window.getSessionData = (key) => {
    return sessionStorage.getItem(key);
};