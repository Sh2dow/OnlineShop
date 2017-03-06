var PagedGridModel = function (items) {
    this.items = ko.observableArray(items);

    this.sortByPrimaryCategoryName = function () {
        this.items.sort(function (a, b) {
            return a.PrimaryCategoryName < b.PrimaryCategoryName ? -1 : 1;
        });
    };

    this.sortByName = function () {
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
        { headerText: "Image", row: "EndTime" }
        ],
        pageSize: 15
    });
};

//ko.applyBindings(new PagedGridModel(vm));
ko.applyBindings(new PagedGridModel(viewModel));
