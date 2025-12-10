let user = {
    name : "ayansh",
    age:21,
    course:"js"
};

let jsonFormat=JSON.stringify(user);

console.log(jsonFormat);

let objectFormat = JSON.parse(jsonFormat);

console.log(objectFormat);