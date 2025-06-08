function isValidDate(dateStr) {
  const dateRegex = new RegExp(
      "^(?:" +
      "(?:0?[1-9]|[12]\\d|3[01])[-/.](?:0?[1-9]|1[0-2])[-/.](?:\\d{1,4})|" + // день-месяц-год
      "(?:\\d{1,4})[-/.](?:0?[1-9]|1[0-2])[-/.](?:0?[1-9]|[12]\\d|3[01])|" + // год-месяц-день
      "(?:0?[1-9]|[12]\\d|3[01])\\s(?:января|февраля|марта|апреля|мая|июня|июля|августа|сентября|октября|ноября|декабря)\\s\\d{1,4}|" + // день месяц_rus год
      "(?:January|February|March|April|May|June|July|August|September|October|November|December)\\s(?:0?[1-9]|[12]\\d|3[01]),\\s\\d{1,4}|" + // Месяц_eng день, год
      "(?:Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)\\s(?:0?[1-9]|[12]\\d|3[01]),\\s\\d{1,4}|" + // Mec_eng день, год
      "(?:\\d{1,4}),\\s(?:January|February|March|April|May|June|July|August|September|October|November|December)\\s(?:0?[1-9]|[12]\\d|3[01])|" + // год, Месяц_eng день
      "(?:\\d{1,4}),\\s(?:Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)\\s(?:0?[1-9]|[12]\\d|3[01])" + // год, Mec_eng день
      ")$",
      "i"
  );

  return dateRegex.test(dateStr);
}
console.log('---------------------')
console.log(isValidDate("14.09.2023"));
console.log(isValidDate("28/02/2024"));
console.log(isValidDate("2023-09-14"));
console.log(isValidDate("14 сентября 2023"));
console.log(isValidDate("Sep 14, 2023"));
console.log(isValidDate("2023, September 14"));

console.log(isValidDate("25.08-1002"));
console.log(isValidDate("декабря 19 1838"));
console.log(isValidDate("8.20.1973"));
console.log(isValidDate("Jun 7, -1563"));