function isValidColor(colorStr) {
    const colorRegex = new RegExp(
        "^(#([0-9a-fA-F]{3}|[0-9a-fA-F]{6})|" + //hex
        "rgb\\((" +
        "(?:\\s*\\d{1,3}\\s*,\\s*){2}\\d{1,3}\\s*|" + //rgb(0-255)
        "(?:\\s*\\d{1,3}%\\s*,\\s*){2}\\d{1,3}%\\s*)\\)|" + //rgb(0%-100%)
        "hsl\\((" +
        "\\s*\\d{1,3}\\s*,\\s*" + //hsl(0-360)
        "\\s*\\d{1,3}%\\s*,\\s*" + //(0%-100%)
        "\\s*\\d{1,3}%\\s*" + //(0%-100%)
        ")\\))$", "i"
    );

    if (!colorRegex.test(colorStr)) return false;

    if (colorStr.startsWith("rgb")) {
        const values = colorStr.match(/\d+%?/g);
        return values.every(val =>
            val.includes("%") ? parseInt(val) <= 100 : parseInt(val) <= 255
        );
    }

    if (colorStr.startsWith("hsl")) {
        const values = colorStr.match(/\d+%?/g);
        const h = parseInt(values[0]);
        return h >= 0 && h <= 360 &&
            values.slice(1).every(val => val.includes("%") && parseInt(val) <= 100);
    }

    return true;
}
console.log('---------------------')
console.log(isValidColor("#21f48D"));
console.log(isValidColor("#888"));
console.log(isValidColor("rgb(255, 255, 255)"));
console.log(isValidColor("rgb(10%, 20%, 0%)"));
console.log(isValidColor("hsl(200,100%,50%)"));
console.log(isValidColor("hsl(0, 0%, 0%)"));

console.log(isValidColor("#2345"));
console.log(isValidColor("ffffff"));
console.log(isValidColor("rgb(257, 50, 10)"));
console.log(isValidColor("hsl(20, 10, 0.5)"));
console.log(isValidColor("hsl(34%, 20%, 50%)"));