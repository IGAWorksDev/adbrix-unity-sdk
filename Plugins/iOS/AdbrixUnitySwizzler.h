#import <Foundation/Foundation.h>
#import <objc/runtime.h>

@interface AdbrixUnitySwizzler : NSObject

void adbrix_swizzle(Class adbrixClass, SEL adbrixSel, Class orgClass, SEL orgSel);

@end