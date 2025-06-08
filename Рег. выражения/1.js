function isValidPassword(password) {
    if (password.length < 8) {
        return false;
    }

    let hasUpperCase = false;
    let hasLowerCase = false;
    let hasDigit = false;
    const specialCharsSet = new Set();
    const specialChars = "^$%@#&*!?";

    for (let i = 0; i < password.length; i++) {
        const char = password[i];

        if (char >= 'A' && char <= 'Z') {
            hasUpperCase = true;
        } else if (char >= 'a' && char <= 'z') {
            hasLowerCase = true;
        } else if (char >= '0' && char <= '9') {
            hasDigit = true;
        } else if (specialChars.includes(char)) {
            specialCharsSet.add(char);
        } else {
            return false;
        }

        if (i > 0 && char === password[i - 1]) {
            return false;
        }
    }

    if (!hasUpperCase) {
        return false;
    }
    if (!hasLowerCase) {
        return false;
    }
    if (!hasDigit) {
        return false;
    }

    if (specialCharsSet.size < 2) {
        return false;
    }

    return true;
}

const passwords = [
    "rtG3FG!Tr^e",
    "aA1!*!1Aa",
    "oF^a1D@y5e6",
    "enroi#$rkdeR#$092uwedchf34tguv394h",
    "пароль",
    "password",
    "qwerty",
    "IOngpu$$W0Rd",
    "!a!m,Shjk35"
];

console.log('---------------------')
passwords.forEach(pass => {
    console.log(`Пароль "${pass}" ${isValidPassword(pass) ? 'корректен' : 'некорректен'}.`);
});