for (var i = 0; i < document.getElementsByName('specRemoveButton').length; i++) {
    document.getElementsByName('specRemoveButton')[i].addEventListener('click', function () {

        if (confirm("Are you sure that you want to Remove this specialization")) {

            //trigger the controller
            let id = document.getElementById('specId').value;
            window.location.href = 'RemoveSpecialization' + '/' + id;
        }

    });
}

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