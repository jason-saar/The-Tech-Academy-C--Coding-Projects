var ViewModel = function () {
    var self = this;
    self.books = ko.observableArray();      // observable data-binding using knockoutjs
    self.error = ko.observable();         
    self.detail = ko.observable(); 
    self.authors = ko.observableArray();
    self.newBook = {
        Author: ko.observable(),
        Genre: ko.observable(),
        Price: ko.observable(),
        Title: ko.observable(),
        Year: ko.observable()
    }
    self.newAuthor = {
        Name: ko.observable()
    }

    var authorsUri = '/api/authors/';
    var booksUri = '/api/books/';

    self.clear = function () {
        self.newBook.Title(null);
        self.newBook.Year(null);
        self.newBook.Genre(null);
        self.newBook.Price(null);
    }

    function getAuthors() {
        ajaxHelper(authorsUri, 'GET').done(function (data) {
            self.authors(data);
        });
    }

    self.addAuthor = function (formElement) {
        var author = {
            Name: self.newAuthor.Name()
        };

        ajaxHelper(authorsUri, 'POST', author).done(function (item) {
            self.authors.push(item)
        });
    }
 

    self.addBook = function (formElement) {
        var book = {
            AuthorId: self.newBook.Author().Id,
            Genre: self.newBook.Genre(),
            Price: self.newBook.Price(),
            Title: self.newBook.Title(),
            Year: self.newBook.Year()
        };

        ajaxHelper(booksUri, 'POST', book).done(function (item) {
            self.books.push(item);
        });
    }

    self.removeBook = function (item) {
        ajaxHelper(booksUri + item.Id, 'DELETE').done(function (item) {
            self.books.remove(item);
            getAllBooks();
        });
    }

    self.getBookDetail = function (item) {
        ajaxHelper(booksUri + item.Id, 'GET').done(function (data) {
            self.detail(data)
        })
    }

    function ajaxHelper(uri, method, data) {
        self.error('');         // Clear Error Message
        return $.ajax({
            type: method,
            url: uri,
            dataType: 'json',
            contentType: 'application/json',
            data: data ? JSON.stringify(data) : null
        }).fail(function (jqXHR, textStatus, errorThrown) {
            self.error(errorThrown);
        });
    }

    // AJAX call to get list of books and push result onto books array
    function getAllBooks() {
        ajaxHelper(booksUri, 'GET').done(function (data) {
            self.books(data);
        });
    }

    // Fetch the initial data.
    getAllBooks();
    getAuthors();
};
// Setup databinding using viewmodel as parameter
ko.applyBindings(new ViewModel());      