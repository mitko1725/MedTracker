for (var i = 0; i < document.getElementsByName('specRemoveButton').length; i++) {
    document.getElementsByName('specRemoveButton')[i].addEventListener('click', function () {

        if (confirm("Are you sure that you want to Remove this specialization")) {

            //trigger the controller
            let id = document.getElementById('specId').value;
            window.location.href = 'RemoveSpecialization' + '/' + id;
        }

    });
}
const select = document.querySelectorAll('.testov');
console.log(select);
//document.getElementsByClassName('testov').addEventListener('mouseover', function () {
//    console.log(document.getElementsByClassName('testov').namedItem[i]);
//})

document.getElementsByClassName('testov').item(1).addEventListener('mouseover', function () {

    console.log("miiiiiiiiitko")


});
for (var i = 0; i < select.length; i++) {
    document.getElementsByClassName('testov')[i].addEventListener('mouseover', function () {
      
       


    });
    var number = i;
    if (number) {
        document.getElementsByClassName('testov')[i].addEventListener('mouseover', function () {
            console.log('this is a test')
            document.getElementsByClassName('testov')[number].style.transform = 'scale(1.1)';
            document.getElementsByClassName('testov')[number].style.transition = 'all 1.1s';


        });

        document.getElementsByClassName('testov')[i].addEventListener('mouseout', function () {

            document.getElementsByClassName('testov')[number].style.transform = 'scale(1.0)';
            document.getElementsByClassName('testov')[number].style.transition = 'all 1.1s';
        })
    }
    
   
}

//document.getElementsByClassName("testov")[0].addEventListener("mouseover", function () {
//   document.getElementsByClassName("testov")[0].style.transform = "scale(1.1)";
//    document.getElementsByClassName("testov")[0].style.transition = "all 1.1s";
//    let counter = 0;
//    console.log(counter++);
//})

//document.getElementsByClassName("testov")[0].addEventListener("mouseout", function () {
//    document.getElementsByClassName("testov")[0].style.transform = "scale(1.0)";
//    document.getElementsByClassName("testov")[0].style.transition = "all 1.1s";
//    let counter = -1;
//    console.log(counter--);
//})

//document.getElementsByClassName("testov")[1].addEventListener("mouseover", function () {
//    document.getElementsByClassName("testov")[1].style.transform = "scale(1.1)";
//    document.getElementsByClassName("testov")[1].style.transition = "all 1.1s";
//    let counter = 0;
//    console.log(counter++);
//})

//document.getElementsByClassName("testov")[1].addEventListener("mouseout", function () {
//    document.getElementsByClassName("testov")[1].style.transform = "scale(1.0)";
//    document.getElementsByClassName("testov")[1].style.transition = "all 1.1s";
//    let counter = -1;
//    console.log(counter--);
//})