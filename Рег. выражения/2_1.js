"use strict";

function isValidBrackets(expression) {
    const arr = [];
    const brackets = {
        '(': ')',
        '{': '}',
        '[': ']'
    };
    
    for (let char of expression) {
        if (brackets[char]) {
            arr.push(char);
        } else {
            const last = arr.pop();
            if (brackets[last] !== char) {
                return false;
            }
        }
    }
    
    return arr.length === 0;
}
console.log('---------------------')
console.log(isValidBrackets("()"));
console.log(isValidBrackets("{[{[()]}]}"));
console.log(isValidBrackets("()[]()"));
console.log('---------------------')