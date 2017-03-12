/***** ko-chart.js *****/
/*global ko, Chart */

(function (ko, Chart) {
    ko.bindingHandlers.barClick = {
        init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
            if (!allBindings.has('chartData')) {
                throw Error('chartType must be used in conjunction with chartData and (optionally) chartOptions');
                return;
            }
            var chartType = allBindings.get('chartType');
            if (chartType !== 'Bar') {
                throw Error('barClick can only be used with chartType Bar');
                return;
            }
        },
        update: function (element, valueAccesor, allBindings, viewModel, bindingContext) { }
    };
    ko.bindingHandlers.lineClick = {
        init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
            if (!allBindings.has('chartData')) {
                throw Error('chartType must be used in conjunction with chartData and (optionally) chartOptions');
                return;
            }
            var chartType = allBindings.get('chartType');
            if (chartType !== 'Line') {
                throw Error('lineClick can only be used with chartType Line');
                return;
            }
        },
        update: function (element, valueAccesor, allBindings, viewModel, bindingContext) { }
    };
    ko.bindingHandlers.segmentClick = {
        init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
            if (!allBindings.has('chartData')) {
                throw Error('chartType must be used in conjunction with chartData and (optionally) chartOptions');
                return;
            }
            var chartType = allBindings.get('chartType');
            if (chartType !== 'Pie' && chartType !== 'Doughnut') {
                throw Error('segmentClick can only be used with chartType Pie or Donut');
                return;
            }
        },
        update: function (element, valueAccesor, allBindings, viewModel, bindingContext) { }
    };
    ko.bindingHandlers.chartType = {
        init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
            if (!allBindings.has('chartData')) {
                throw Error('chartType must be used in conjunction with chartData and (optionally) chartOptions');
            }
        },
        update: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
            var ctx = element.getContext('2d'),
                type = ko.unwrap(valueAccessor()),
                data = ko.unwrap(allBindings.get('chartData')),
                options = ko.unwrap(allBindings.get('chartOptions')) || {},
            	segmentClick = ko.unwrap(allBindings.get('segmentClick')),
                barClick = ko.unwrap(allBindings.get('barClick')),
                lineClick = ko.unwrap(allBindings.get('lineClick'));

            /* NB: Fix for newer knockout (see https://gist.github.com/jmhdez/4987b053e817d65d7c68)
			if (this.chart) {
				this.chart.destroy();
				delete this.chart;
			}
			
			this.chart = new Chart(ctx)[type](data, options);
			//*/

            ko.utils.domNodeDisposal.addDisposeCallback(element,

            function () {
                $(element).chart.destroy();
                delete $(element).chart;
            });

            var newChart = new Chart(ctx)[type](data, options);
            var $element = $(element)[0];
            $element.chart = newChart;
            //* End of fix

            //* Remove existing click binding
            if ($element.click) {
                $element.removeEventListener('click', $element.click);
                delete ($element.click);
            }
            //* Add segment click binding
            switch (type) {
                case "Pie":
                case "Doughnut":
                    if (segmentClick) {
                        $element.click = function (evt) {
                            var activePoints = newChart.getSegmentsAtEvent(evt);
                            segmentClick(activePoints[0], newChart);
                        };
                    }
                    break;
                case "Bar":
                    if (barClick) {
                        $element.click = function (evt) {
                            barClick(newChart.getBarsAtEvent(evt), newChart);
                        };
                    }
                    break;
                case "Line":
                    if (lineClick) {
                        $element.click = function (evt) { lineClick(newChart.getPointsAtEvent(evt), newChart); };
                    }
                    break;
                default:
                    break;
            }
            $element.addEventListener('click', $element.click);
        }
    };

    ko.bindingHandlers.chartData = {
        init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
            if (!allBindings.has('chartType')) {
                throw Error('chartData must be used in conjunction with chartType and (optionally) chartOptions');
            }
        }
    };

    ko.bindingHandlers.chartOptions = {
        init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
            if (!allBindings.has('chartData') || !allBindings.has('chartType')) {
                throw Error('chartOptions must be used in conjunction with chartType and chartData');
            }
        }
    };

})(ko, Chart);
/***** End of ko-chart.js *****/

function KoModel(data) {
    var self = this;
    self.machines = ko.observableArray(data.machines);
    self.browserData = ko.observable(data.browsers || null);
    self.lineData = ko.observable(data.lineData || null);
    self.alertLabel = function (chartItem, chart) {
        alert(chartItem.label);
    };
    self.alertJson = function (chartItem, chart) {
        alert(JSON.stringify(chartItem));
    };

    self.setBrowserData1 = function () {
        self.browserData({
            labels: ["January", "February", "March", "April"],
            datasets: [{
                label: 'IE',
                fillColor: 'rgba(250, 50, 50, 0.5)',
                strokeColor: 'rgba(250, 50, 50, 0.8)',
                highlightFill: 'rgba(250, 50, 50, 0.75)',
                highlightStroke: 'rgba(250, 50, 50, 1)',
                data: [60, 50, 45, 32]
            }, {
                label: 'Chrome',
                fillColor: 'rgba(50, 250, 50, 0.5)',
                strokeColor: 'rgba(50, 250, 50, 0.8)',
                highlightFill: 'rgba(50, 250, 50, 0.75)',
                highlightstroke: 'rgba(50, 250, 50, 1)',
                data: [10, 12, 16, 30]
            }]
        });
    };
    self.setDataSet1 = function () {
        self.machines([{
            value: 100,
            color: '#FF3333',
            highlight: '#FF7777',
            label: 'PC'
        }, {
            value: 75,
            color: '#3333FF',
            highlight: '#7777FF',
            label: 'Android'
        }]);
    };
    self.setDataSet2 = function () {
        self.machines([{
            value: 30,
            color: '#FF3333',
            highlight: '#FF7777',
            label: 'PC'
        }, {
            value: 85,
            color: '#3333FF',
            highlight: '#7777FF',
            label: 'Android'
        }, {
            value: 15,
            color: '#33FF33',
            highlight: '#77FF77',
            label: 'Linux'
        }]);
    };
    self.setLineData1 = function () {
        self.lineData({
            labels: ["January", "February", "March", "April", "May", "June", "July"],
            datasets: [
                {
                    label: "My First dataset",
                    fillColor: "rgba(220,220,220,0.2)",
                    strokeColor: "rgba(220,220,220,1)",
                    pointColor: "rgba(220,220,220,1)",
                    pointStrokeColor: "#fff",
                    pointHighlightFill: "#fff",
                    pointHighlightStroke: "rgba(220,220,220,1)",
                    data: [65, 59, 80, 81, 56, 55, 40]
                },
                {
                    label: "My Second dataset",
                    fillColor: "rgba(151,187,205,0.2)",
                    strokeColor: "rgba(151,187,205,1)",
                    pointColor: "rgba(151,187,205,1)",
                    pointStrokeColor: "#fff",
                    pointHighlightFill: "#fff",
                    pointHighlightStroke: "rgba(151,187,205,1)",
                    data: [28, 48, 40, 19, 86, 27, 90]
                }
            ]
        });
    };
    self.setLineData2 = function () {
        self.lineData({
            labels: ["January", "February", "March", "April", "May", "June", "July"],
            datasets: [
                {
                    label: "My First dataset",
                    fillColor: "rgba(220,120,220,0.2)",
                    strokeColor: "rgba(220,120,220,1)",
                    pointColor: "rgba(220,120,220,1)",
                    pointStrokeColor: "#fff",
                    pointHighlightFill: "#fff",
                    pointHighlightStroke: "rgba(220,220,220,1)",
                    data: [75, 69, 90, 91, 66, 65, 50]
                }
            ]
        });
    };
    if (!self.lineData()) {
        self.setLineData1();
    }
};

//$(document).ready(function () {
//    var data = {};
//    var koModel = new KoModel(data);
//    ko.applyBindings(koModel);
//});