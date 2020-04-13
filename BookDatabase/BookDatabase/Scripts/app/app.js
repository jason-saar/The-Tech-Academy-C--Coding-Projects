(function () {

    ko.observable.fn.store = function () {
        var self = this;
        var oldValue = self();

        var observable = ko.computed({
            read: function () {
                return self();
            },
            write: function (value) {
                oldValue = self();
                self(value);
            }
        });

        this.revert = function () {
            self(oldValue);
        }
        this.commit = function () {
            oldValue = self();
        }
        return this;
    }

    //Convert JSON representation of a book into equivalent object with observables
    function book(data) {
        var self = this;
        data = data || {};
        //Data from model
        self.Id = data.Id;
        self.Title = ko.observable(data.Title);
        self.Author = ko.observable(data.Author);
        self.Genre = ko.observable(data.Genre);
        self.Year = ko.observable(data.Year);
        self.Rating = ko.observable(data.Rating);
    };

    var ViewModel = function () {
        var self = this;
        //View model observables
        self.books = ko.observableArray();
        self.error = ko.observable();
        self.genre = ko.observable();   //Genre that is being browsed
        //Genres
        self.genres = ['Action', 'Comedy', 'Fantasy', 'Horror', 'Mystery', 'Sci-Fi'];
        //Add a JSON array of books to the view model
        function addBooks(data) {
            var mapped = ko.utils.ArrayMap(data, function (item) {
                return new book(item);
            });
            self.books(mapped);
        }
        //Callback for error responses from server
        function onError(error) {
            self.error('Error: ' + error.status + ' ' + error.statusText);
        }
        //Fetches a list of books by genre and updates the view model
        self.getByGenre = function (genre) {
            self.error(''); // Clear the error
            self.genre(genre);
            app.service.byGenre(genre).then(addBooks, onError);
        };
        //Initialize app by getting the first genre from genres array
        self.getByGenre(self.genres[0]);
    }

    //Create view model instance and pass it to Knockout
    ko.applyBindings(new ViewModel());

})();
