var PagedGridModel = function (items) {
    this.items = ko.observableArray(items);

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
        { headerText: "PrimaryCategoryID", rowText: "PrimaryCategoryID" },
        { headerText: "EndTime", rowText: "EndTime" }
        ],
        pageSize: 15
    });
};

//ko.applyBindings(viewModel);
ko.applyBindings(new PagedGridModel(vm.Count > 0 ? vm : null));