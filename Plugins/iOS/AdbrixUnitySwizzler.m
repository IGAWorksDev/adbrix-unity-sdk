#import "AdbrixUnitySwizzler.h"

@implementation AdbrixUnitySwizzler

void adbrix_swizzle(Class adbrixClass, SEL adbrixSel, Class orgClass, SEL orgSel)
{
    Method adbrixMethod = class_getInstanceMethod(adbrixClass, adbrixSel);
    IMP adbrixImp = method_getImplementation(adbrixMethod);
    const char* adbrixMethodType = method_getTypeEncoding(adbrixMethod);
    
    BOOL didAddNewMethod = class_addMethod(orgClass, orgSel, adbrixImp, adbrixMethodType);
    
    if (!didAddNewMethod) {
        class_addMethod(orgClass, adbrixSel, adbrixImp, adbrixMethodType);
        
        adbrixMethod = class_getInstanceMethod(orgClass, adbrixSel);
        Method orgMethod = class_getInstanceMethod(orgClass, orgSel);
        
        method_exchangeImplementations(adbrixMethod, orgMethod);
    }
}
@end