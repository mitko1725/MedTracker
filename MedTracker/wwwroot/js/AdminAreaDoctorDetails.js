
document.getElementById("rejectButton").addEventListener("click", function () {

    if (confirm("Are you sure that you want to REJECT this Doctor")) {
        let form = document.getElementById("rejectDocForm");
        //trigger the controller
        form.submit();

    }

})


document.getElementById("approveButton").addEventListener("click", function () {


    let form = document.getElementById("approveDocForm");
    //trigger the controller
    form.submit();


})