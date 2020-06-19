"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.EnumHelpers = void 0;
var EnumHelpers = /** @class */ (function () {
    function EnumHelpers() {
    }
    EnumHelpers.getNamesAndValues = function (e) {
        return EnumHelpers.getNames(e).map(function (n) { return ({ name: n, value: e[n] }); });
    };
    EnumHelpers.getNames = function (e) {
        return EnumHelpers.getObjValues(e).filter(function (v) { return typeof v === 'string'; });
    };
    EnumHelpers.getValues = function (e) {
        return EnumHelpers.getObjValues(e).filter(function (v) { return typeof v === 'number'; });
    };
    EnumHelpers.getSelectList = function (e, stringConverter) {
        var selectList = new Map();
        this.getValues(e).forEach(function (val) { return selectList.set(val, stringConverter(val)); });
        return selectList;
    };
    EnumHelpers.getSelectListAsArray = function (e, stringConverter) {
        return Array.from(this.getSelectList(e, stringConverter), function (value) { return ({ value: value[0], presentation: value[1] }); });
    };
    EnumHelpers.toString = function (dir, e) {
        return e[dir];
    };
    EnumHelpers.getObjValues = function (e) {
        return Object.keys(e).map(function (k) { return e[k]; });
    };
    return EnumHelpers;
}());
exports.EnumHelpers = EnumHelpers;
//# sourceMappingURL=enumhelpers.js.map