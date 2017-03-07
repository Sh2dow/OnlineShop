var PagedGridModel = function (jsonarr) {
    this.items = ko.observableArray(jsonarr);

    this.sortByPrimaryCategoryName = function () {
        this.items.sort(function (a, b) {
            return a.PrimaryCategoryName < b.PrimaryCategoryName ? -1 : 1;
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
        { headerText: "Price", rowText: "Price" },
        { headerText: "PrimaryCategoryID", rowText: "PrimaryCategoryID" },
        { headerText: "PrimaryCategoryName", rowText: "PrimaryCategoryName" },
        { headerText: "EndTime", rowText: "EndTime" },
        //{ headerText: "Image", rowText: "Image" }
        ],
        pageSize: 10
    });
};