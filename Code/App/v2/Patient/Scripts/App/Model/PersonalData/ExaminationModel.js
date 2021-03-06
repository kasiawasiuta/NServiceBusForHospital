﻿function ExaminationModel(data) {
    var self = this;

    this.Id = ko.observable(data.Id);
    this.PatientDieseaseId = ko.observable(data.PatientDieseaseId);
    this.Comment = ko.observable(data.Comment);
    this.When = ko.observable(data.When);
    this.ExaminationType = ko.observable(data.ExaminationType);
    this.LogType = ko.observable(data.LogType);

    this.ExaminationName = ko.computed(function () {
        return getExaminationTypeName(self.ExaminationType());
    }, this);

    this.LogName = ko.computed(function () {
        return getLogTypeName(self.LogType());
    }, this);

    this.ExaminationCssClassName = ko.computed(function () {
        return getExaminationTypeName(self.ExaminationType()) + "-" + getLogTypeName(self.LogType());
    }, this);
};