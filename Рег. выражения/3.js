function tokenize(expression) {
    const constants = new Set(['pi', 'e', 'sqrt2', 'In2', 'In10']);
    const functions = new Set(['sin', 'cos', 'tg', 'ctg', 'tan', 'cot', 'sinh', 'cosh', 'th', 'cth', 'tanh', 'coth', 'In', 'Ig', 'log', 'exp', 'sqrt', 'cbrt', 'abs', 'sign']);
    const operators = new Set(['^', '*', '/', '+', '-']);
    
    const tokens = [];
    let i = 0;
    const length = expression.length;

    while (i < length) {
        const char = expression[i];

        if (char === ' ' || char === '\t' || char === '\n' || char === '\r') {
            i++;
            continue;
        }

        if (isLetter(char)) {
            let j = i + 1;
            while (j < length && (isLetterOrDigit(expression[j]))) {
                j++;
            }
            const identifier = expression.substring(i, j);
            if (constants.has(identifier)) {
                tokens.push({ type: 'constant', span: [i, j] });
            } else if (functions.has(identifier)) {
                tokens.push({ type: 'function', span: [i, j] });
            } else {
                tokens.push({ type: 'variable', span: [i, j] });
            }
            i = j;
        } else if (isDigit(char)) {
            let j = i + 1;
            let hasDot = false;
            while (j < length && (isDigit(expression[j]) || (expression[j] === '.' && !hasDot))) {
                if (expression[j] === '.') {
                    hasDot = true;
                }
                j++;
            }
            tokens.push({ type: 'number', span: [i, j] });
            i = j;
        } else if (operators.has(char)) {
            tokens.push({ type: 'operator', span: [i, i + 1] });
            i++;
        } else if (char === '(') {
            tokens.push({ type: 'left_parenthesis', span: [i, i + 1] });
            i++;
        } else if (char === ')') {
            tokens.push({ type: 'right_parenthesis', span: [i, i + 1] });
            i++;
        } else {
            throw new Error(`Unexpected character at position ${i}: ${char}`);
        }
    }
    return tokens;
}

function isLetter(char) {
    return (char >= 'a' && char <= 'z') || (char >= 'A' && char <= 'Z');
}

function isDigit(char) {
    return char >= '0' && char <= '9';
}

function isLetterOrDigit(char) {
    return isLetter(char) || isDigit(char);
}

let expression = "sin(x   ) + cos(y) * 2.5";
let expression2 = "pi     +           Uajsdkl23i";
let expression3 = "(     63333.93 /8505    )";

console.log('---------------------')
console.log(expression);
console.log(tokenize(expression));

console.log(expression2);
console.log(tokenize(expression2));

console.log(expression3);
console.log(tokenize(expression3));