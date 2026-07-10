// Piccolo ponte tra C# e il localStorage del browser
export function getItem(key) {
    return localStorage.getItem(key);
}

export function setItem(key, value) {
    localStorage.setItem(key, value);
}