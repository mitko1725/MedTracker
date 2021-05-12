for (var i = 0; i < document.getElementsByName('specRemoveButton').length; i++) {
    document.getElementsByName('specRemoveButton')[i].addEventListener('click', function () {

        if (confirm("Are you sure that you want to Remove this specialization")) {

            //trigger the controller
            let id = document.getElementById('specId').value;
            window.location.href = 'RemoveSpecialization' + '/' + id;
        }

    });
}

//document.getElementsByClassName('testov').addEventListener('mouseover', function () {
//    console.log(document.getElementsByClassName('testov').namedItem[i]);
//})

var tableElements = document.querySelectorAll(".testov");

tableElements.forEach(function (element) {
    element.addEventListener('mouseover', function () {
            
       element.style.transform = 'scale(1.15)';
        element.style.transition = 'all 1.1s';


        });

    element.addEventListener('mouseout', function () {

            element.style.transform = 'scale(1.0)';
            element.style.transition = 'all 1.1s';
        })
});
