var ViewModel;
var sessionId;

function Game(index, name) {
    var self = this;
    self.Id = ko.observable(index);
    self.Name = ko.observable(name);
}

function GamesList() {
    var self = this;
    self.games = ko.observableArray([]);
    self.selected = 0;

    self.fClick = function (data, event) {
        self.selected = ko.toJS(data).Id;
        ViewModel.achievementModel.getAchievements();
    };

    self.getGames = function () {
        self.games.removeAll();
        $.getJSON('/api/game/' + sessionId + '/', function (data) {
            $.each(data, function (key, value) {
                self.games.push(new Game(value.Id, value.Name));
            });
            if (self.games().length > 0) {
                self.selected = ko.toJS(self.games()[0]).Id;
                ViewModel.achievementModel.getAchievements();
            }
                
        });
    };
}

function Achievement(index, player, achievement, dateTime) {
    var self = this;
    self.Index = ko.observable(index);
    self.Player = ko.observable(player);
    self.Achievement = ko.observable(achievement);
    self.DateTime = ko.observable(dateTime);
}

function AchievementsList() {
    var self = this;
    var timeInterval = { day: 1, week: 2, allTime: 3 };
    self.achievements = ko.observableArray([]);

    self.timeTabs = ko.observableArray([
        { Text: "All time", Id: timeInterval.allTime },
        { Text: "Week", Id: timeInterval.week },
        { Text: "Day", Id: timeInterval.day }
    ]);

    self.selectedTab = timeInterval.allTime;

    self.fClick = function (data, event) {
        self.selectedTab = ko.toJS(data).Id;
        ViewModel.achievementModel.getAchievements();
    };

    self.getAchievements = function () {
        self.achievements.removeAll();
        $.getJSON('/api/achievement/' +
                   ViewModel.gameModel.selected + '/' +
                   self.selectedTab + '/' +
                   sessionId + '/', function (data) {
            $.each(data, function (key, value) {
                self.achievements.push(new Achievement(key + 1, value.Player, value.Achieve, value.DateTimeString));
            });
        });
    };
}

ViewModel = {
    achievementModel: new AchievementsList(),
    gameModel: new GamesList()
};


$(document).ready(function () {

    if (window.sessionStorage){
        sessionId = window.sessionStorage.getItem('sessionId');
        if (!sessionId) {
            sessionId = Math.random();
            sessionStorage['sessionId'] = sessionId;
        }
    }

    ko.applyBindings(ViewModel);
    ViewModel.gameModel.getGames();
});


