System.register(["@angular/core", "@angular/http", "@ng-bootstrap/ng-bootstrap", "moment", "../common/common"], function (exports_1, context_1) {
    "use strict";
    var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
        var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
        if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
        else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
        return c > 3 && r && Object.defineProperty(target, key, r), r;
    };
    var __metadata = (this && this.__metadata) || function (k, v) {
        if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
    };
    var __moduleName = context_1 && context_1.id;
    var core_1, http_1, ng_bootstrap_1, moment, common_1, ModalContentComponent;
    return {
        setters: [
            function (core_1_1) {
                core_1 = core_1_1;
            },
            function (http_1_1) {
                http_1 = http_1_1;
            },
            function (ng_bootstrap_1_1) {
                ng_bootstrap_1 = ng_bootstrap_1_1;
            },
            function (moment_1) {
                moment = moment_1;
            },
            function (common_1_1) {
                common_1 = common_1_1;
            }
        ],
        execute: function () {
            ModalContentComponent = (function () {
                function ModalContentComponent(activeModal, http, classService) {
                    this.activeModal = activeModal;
                    this.http = http;
                    this.classService = classService;
                }
                ModalContentComponent.prototype.ngOnInit = function () {
                    var _this = this;
                    this.classService.getClass(this.classId)
                        .subscribe(function (data) {
                        _this.subjectName = data.subjectName;
                        _this.type = data.type;
                        _this.dateTime = data.dateTime;
                    });
                    this.classService.getPlaces(this.classId)
                        .subscribe(function (data) { return _this.classrooms = data; });
                    this.classService.getLecturers(this.classId)
                        .subscribe(function (data) { return _this.lecturers = data; });
                };
                ModalContentComponent.prototype.formatDateTime = function (dateTime) {
                    return moment.utc(dateTime).format("DD.MM.YYYY, dddd, HH:mm");
                };
                ModalContentComponent.prototype.formatClassrooms = function (classrooms) {
                    return classrooms
                        ? classrooms.reduce(function (a, c) { return a + ", " + c.name; }, "").substring(2)
                        : "";
                };
                ModalContentComponent.prototype.formatLecturers = function (lecturers) {
                    return lecturers
                        ? lecturers.reduce(function (a, l) { return a + ", " + l.userLastName + " " + l.userFirstName[0] + ". " +
                            (l.userMiddleName[0] + "."); }, "").substring(2)
                        : "";
                };
                return ModalContentComponent;
            }());
            __decorate([
                core_1.Input(),
                __metadata("design:type", Number)
            ], ModalContentComponent.prototype, "classId", void 0);
            ModalContentComponent = __decorate([
                core_1.Component({
                    selector: "student-modal-content",
                    templateUrl: "app/student/calendar/modal-content.component.html"
                }),
                __metadata("design:paramtypes", [ng_bootstrap_1.NgbActiveModal,
                    http_1.Http,
                    common_1.ClassService])
            ], ModalContentComponent);
            exports_1("default", ModalContentComponent);
        }
    };
});
//# sourceMappingURL=modal-content.component.js.map