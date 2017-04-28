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
var core_1 = require("@angular/core");
var common_1 = require("../../common/common");
var ModalMaterialsComponent = (function () {
    function ModalMaterialsComponent(classService) {
        this.materials = [];
        this.classService = classService;
    }
    ModalMaterialsComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.classService.getMaterials(this.classId)
            .subscribe(function (materials) {
            _this.materials = materials;
        });
    };
    return ModalMaterialsComponent;
}());
__decorate([
    core_1.Input(),
    __metadata("design:type", Number)
], ModalMaterialsComponent.prototype, "classId", void 0);
ModalMaterialsComponent = __decorate([
    core_1.Component({
        selector: "student-modal-materials",
        templateUrl: "app/student/calendar/modal/modal-materials.component.html",
        styleUrls: ["app/student/calendar/modal/modal-materials.component.css"]
    }),
    __metadata("design:paramtypes", [common_1.ClassService])
], ModalMaterialsComponent);
Object.defineProperty(exports, "__esModule", { value: true });
exports.default = ModalMaterialsComponent;
//# sourceMappingURL=modal-materials.component.js.map