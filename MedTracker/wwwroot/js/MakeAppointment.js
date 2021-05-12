
var doctorId = document.getElementById('doctorId').value;

document.addEventListener('readystatechange', event => {

    // When HTML/DOM elements are ready:
    if (event.target.readyState === "interactive") {   //does same as:  ..addEventListener("DOMContentLoaded"..
        loadEvents();
        $('.opening-hours li').eq(new Date().getDay()).addClass('today');
    }

    // When window loaded ( external resources are loaded too- `css`,`src`, etc...)
    if (event.target.readyState === "complete") {
        loadEvents();
        $('.opening-hours li').eq(new Date().getDay()).addClass('today');
    }
});



var nav = new DayPilot.Navigator("nav");
nav.showMonths = 2;
nav.skipMonths = 2;
nav.selectMode = "week";
nav.init();

var dp = new DayPilot.Calendar("dp");
dp.viewType = "Week";
dp.start = '';


nav.onTimeRangeSelected = function (args) {
    dp.startDate = args.day;
    dp.update();
    loadEvents();
};

function loadEvents() {

    DayPilot.Http.ajax({

        url: "/api/CalendarEvents?StartDate=" + dp.visibleStart() + "&EndDate=" + dp.visibleEnd() + "&DoctorId=" + parseInt(doctorId),   // in .NET, use "/api/CalendarEvents"
        success: function (obj) {


            console.log(obj.data),
                console.log(obj.data[0]),
                console.log(typeof (obj.data)),


                dp.events.list = [
             
                {
                        "start": obj.data[0].startDate,
                        "end": obj.data[0].endDate,
                        "Id": obj.data[0].Id,
                        "Reason": "Calendar Event TESTT"
                    }],
                dp.update()
        }

    });
}
dp.init();