var x1 = 1;
var x2 = "aaa";
var x3 = true;
var x4 = new Date(2025, 6, 6);
var x5 = [1, 2, 3];
var x6 = { prop1: 1, prop2: "aaa" };
// x1 = "aaa";
var MyEnum;
(function (MyEnum) {
    MyEnum[MyEnum["MyEnum1"] = 0] = "MyEnum1";
    MyEnum[MyEnum["MyEnum2"] = 1] = "MyEnum2";
})(MyEnum || (MyEnum = {}));
var x7 = MyEnum.MyEnum1;
var x8 = "val1";
var x9 = 1;
var x10 = 3;
var x11 = {
    prop1: 1,
};
var x12 = 1;
x12 = "aaa";
var x13 = {
    prop: 1
};
var x14 = {
    prop: "aaa"
};
function f1(p1, p2) {
    return p1;
}
