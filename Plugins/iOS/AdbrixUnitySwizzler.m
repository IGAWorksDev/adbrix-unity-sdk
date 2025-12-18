#import <objc/runtime.h>

static BOOL adbrix_noop_bool(id self, SEL _cmd, ...) {
    return YES;
}

void adbrix_swizzle(Class adbrixClass, SEL adbrixSel, Class orgClass, SEL orgSel)
{
    Method adbrixMethod = class_getInstanceMethod(adbrixClass, adbrixSel);
    if (!adbrixMethod) return;

    IMP adbrixImp = method_getImplementation(adbrixMethod);
    const char* adbrixTypes = method_getTypeEncoding(adbrixMethod);

    Method orgMethod = class_getInstanceMethod(orgClass, orgSel);
    IMP orgImp = orgMethod ? method_getImplementation(orgMethod) : (IMP)adbrix_noop_bool;
    const char* orgTypes = orgMethod ? method_getTypeEncoding(orgMethod) : adbrixTypes;

    BOOL didAddNewMethod = class_addMethod(orgClass, orgSel, adbrixImp, adbrixTypes);

    if (didAddNewMethod) {
         class_replaceMethod(orgClass, adbrixSel, orgImp, orgTypes);
        return;
    }

    class_replaceMethod(orgClass, adbrixSel, adbrixImp, adbrixTypes);

    Method adbrixMethodInOrg = class_getInstanceMethod(orgClass, adbrixSel);
    Method orgMethodInOrg = class_getInstanceMethod(orgClass, orgSel);

    if (!adbrixMethodInOrg || !orgMethodInOrg) return;

    method_exchangeImplementations(adbrixMethodInOrg, orgMethodInOrg);
}
