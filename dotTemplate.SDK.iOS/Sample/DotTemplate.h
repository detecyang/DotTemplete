//
//  DotTemplate.h
//  DotTemplate
//
//  Created by yang on 13-03-08.
//  Copyright 2013 detecyang. All rights reserved.
//

#import <Foundation/Foundation.h>


typedef enum
{
    TextAlignmentLeft = 0,
    TextAlignmentCenter,
    TextAlignmentRight
}TextAlignment;

@interface TextBox : NSObject
{
    NSUInteger ID;
    NSString *name;
    NSString *value;
    NSUInteger x, y, width, height;
    TextAlignment align;
}
@property (assign, nonatomic) NSUInteger ID;
@property (copy ,nonatomic) NSString *name;
@property (copy ,nonatomic) NSString *value;
@property (assign, nonatomic) NSUInteger x;
@property (assign, nonatomic) NSUInteger y;
@property (assign, nonatomic) NSUInteger width;
@property (assign, nonatomic) NSUInteger height;
@property (assign, nonatomic) TextAlignment align;
@end






@interface DotTemplate : NSObject
{
    NSString *name;
    NSUInteger width, height;
    NSMutableArray *textBoxArray;
}
@property (copy ,nonatomic) NSString *name;
@property (assign, nonatomic) NSUInteger width;
@property (assign, nonatomic) NSUInteger height;
@property (copy ,nonatomic) NSMutableArray *textBoxArray;

- (id)initWithXmlFile:(NSString *)filePath;
- (id)initWithXmlString:(NSString *)xmlString;
- (BOOL)loadXmlFile:(NSString *)filePath;
- (BOOL)loadXmlString:(NSString *)xmlString;
- (NSMutableArray *)ParseStringsWithTextBox;

+ (int)getCountOfString:(NSString*)string;
@end


