﻿var PagedGridModel = function (items) {
    this.items = ko.observableArray(items);

    this.sortByPrice = function () {
        this.items.sort(function (a, b) {
            return a.Price < b.Price ? -1 : 1;
        });
    };

    this.sortByTitle = function () {
        this.items.sort(function (a, b) {
            return a.Title < b.Title ? -1 : 1;
        });
    };

    this.jumpToFirstPage = function () {
        this.gridViewModel.currentPageIndex(0);
    };

    this.gridViewModel = new ko.simpleGrid.viewModel({
        data: this.items,
        columns: [
        { headerText: "Title", rowText: "Title" },
        { headerText: "Price", rowText: function (item) { return "$" + item.Price.toFixed(2) } , cellStyle: { textAlign: "center" } },
        { headerText: "Category", rowText: "PrimaryCategoryName" },
        //{ headerText: "End time", rowText: "EndTime" },
        { headerText: "Image", rowText: function (item) { return item.Image } },
        ],
        pageSize: 10
    });
};

ko.applyBindings(new PagedGridModel(jsonData));