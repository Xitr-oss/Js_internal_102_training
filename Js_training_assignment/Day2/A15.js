function votingCheck(age){
    if(age>=18){
        console.log("eligible for voting");
    }
    else{
        console.log("not eligible")
    }
}

votingCheck(19);
votingCheck(17);