var chart = new CanvasJS.Chart("chart", {
    theme: "theme1",
    animationEnabled: true,
    title: {
        text: "Time",
        fontSize: 21
    },
    axisX: {
        valueFormatString: "YYYY",
        interval: 1,
        intervalType: "year"
    },
    axisY: {
        title: "USD"
    },

    data: [
    {
        type: "area",
        dataPoints: [
            { x: new Date('2014-11-11T20:42:26+02:00'), y: 2600 },
            { x: new Date('2015-11-05T20:44:18+02:00'), y: 3800 },
            { x: new Date('2016-11-07T16:14:34+02:00'), y: 4300 },
            { x: new Date('2017-03-01T19:15:42+02:00'), y: 2900 },
            { x: new Date('2017-03-06T00:32:57+02:00'), y: 4100 }
        ],

        //You can add dynamic data from the controller as shown below. Check the controller and uncomment the line which generates dataPoints.
        //dataPoints: @Html.Raw(ViewBag.DataPoints),
    }
    ]
});

chart.render();