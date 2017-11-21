//
//  UniPasteBoard.m
//  Unity-iPhone
//
//  Created by 王 巍 on 14-6-10.
//
//

#import <Foundation/Foundation.h>

@interface UniPasteBoard : NSObject

@end

@implementation UniPasteBoard
+(NSString *) stringFromGeneralPasteBoard {
    UIPasteboard * pasteboard = [UIPasteboard generalPasteboard];
    return [pasteboard string];
}

+(void) writeStringToGeneralPasteBoard:(NSString *)text {
    UIPasteboard * pasteboard = [UIPasteboard generalPasteboard];
    [pasteboard setString:text];
}
@end

// Converts C style string to NSString
NSString* UniPasteBoardMakeNSString (const char* string)
{
    if (string) {
        return [NSString stringWithUTF8String: string];
    } else {
        return [NSString stringWithUTF8String: ""];
    }
}

// Helper method to create C string copy
char* UniPasteBoardMakeCString(NSString *str)
{
    const char* string = [str UTF8String];
    if (string == NULL) {
        return NULL;
    }

    char* res = (char*)malloc(strlen(string) + 1);
    strcpy(res, string);
    return res;
}

extern "C" {
    void _setClipBoardString(const char* text) {
        [UniPasteBoard writeStringToGeneralPasteBoard:UniPasteBoardMakeNSString(text)];
    }
    
    const char * _getClipBoardString() {
        return UniPasteBoardMakeCString([UniPasteBoard stringFromGeneralPasteBoard]);
    }
}