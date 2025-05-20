let x1: number = 1;
let x2: string = "aaa";
let x3: boolean = true;
let x4: Date = new Date(2025, 6, 6);
let x5: number[] = [1, 2, 3]
let x6: { prop1: number, prop2: string } = { prop1: 1, prop2: "aaa" }
// x1 = "aaa";

enum MyEnum {
    MyEnum1,
    MyEnum2
}

let x7: MyEnum = MyEnum.MyEnum1;

let x8: "val1" | "val2" = "val1";

let x9: string | number = 1;

type MyType = "val1" | number;

let x10: MyType = 3;

interface MyInterface {
    prop1: number;
    prop2?: string | null;
}

let x11: MyInterface = {
    prop1: 1,
}

let x12: any = 1;
x12 = "aaa";

interface MyInterfaceSecond<T> {
    prop: T | null;
}

let x13: MyInterfaceSecond<number> = {
    prop: 1
}

let x14: MyInterfaceSecond<string> = {
    prop: "aaa"
}

function f1(p1: number, p2: string): number {
    return p1;
}