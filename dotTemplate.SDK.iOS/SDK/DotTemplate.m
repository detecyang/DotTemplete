//
//  DotTemplate.m
//  DotTemplate
//
//  Created by yang on 13-03-08.
//  Copyright 2013 detecyang. All rights reserved.
//

#import "DotTemplate.h"
#import "GDataXMLNode.h"


#define IS_CH_SYMBOL(chr) ((chr)>0x4e00 && (chr)<0x9fff)
#define FLAG_DISABLE 0xffff  //设置一个block字符块为占用的标记


@implementation TextBox
@synthesize ID;
@synthesize name;
@synthesize value;
@synthesize x;
@synthesize y;
@synthesize width;
@synthesize height;
@synthesize align;

- (id)init
{
    self = [super init];
    if (self)
    {
        name = [[NSString alloc] init];
        value = [[NSString alloc] init];
    }
    return self;
}

- (void)dealloc
{
    self.name = nil;
    self.value = nil;
    [super dealloc];
}

@end






#define NODE_TEMPLATE      @"TEMPLATE"
#define NODE_TEXTBOX       @"TEXTBOX"

#define ATTRIBUTE_ID       @"ID"
#define ATTRIBUTE_NAME     @"NAME"
#define ATTRIBUTE_ALIGN    @"ALIGN"
#define ATTRIBUTE_X        @"X"
#define ATTRIBUTE_Y        @"Y"
#define ATTRIBUTE_W        @"W"
#define ATTRIBUTE_H        @"H"

#define ATTRIBUTE_VALUE_L  @"L"
#define ATTRIBUTE_VALUE_C  @"C"
#define ATTRIBUTE_VALUE_R  @"R"


@implementation DotTemplate
@synthesize name;
@synthesize width;
@synthesize height;
@synthesize textBoxArray;

- (id)init
{
    self = [super init];
    if (self)
    {
        name = [[NSString alloc] init];
        textBoxArray = [[NSMutableArray alloc] init];
    }
    return self;
}

- (void)dealloc
{
    self.name = nil;
    self.textBoxArray = nil;
    [super dealloc];
}


- (id)initWithXmlFile:(NSString *)filePath
{
    self = [super init];
    if (self)
    {
        name = [[NSString alloc] init];
        width = 0;
        height = 0;
        textBoxArray = [[NSMutableArray alloc] init];
        [self loadXmlFile:filePath];
    }
    return self;
}

- (id)initWithXmlString:(NSString *)xmlString
{
    self = [super init];
    if (self)
    {
        name = [[NSString alloc] init];
        width = 0;
        height = 0;
        textBoxArray = [[NSMutableArray alloc] init];
        [self loadXmlString:xmlString];
    }
    return self;
}


- (BOOL)loadXmlFile:(NSString *)filePath
{
    NSString *strContent = [[NSString alloc] initWithContentsOfFile:filePath];
    BOOL ret = [self loadXmlString:strContent];
    [strContent release];
    return ret;
}


- (BOOL)loadXmlString:(NSString *)xmlString
{
    GDataXMLDocument *doc;
    
    if (xmlString == nil || [xmlString length] == 0)
    {
        return NO;
    }
    @try
    {
        NSError *error;
        doc = [[GDataXMLDocument alloc] initWithXMLString:xmlString options:0 error:&error];
        //读取模板属性
        GDataXMLElement *templateNode = [[[doc rootElement] elementsForName:NODE_TEMPLATE] objectAtIndex:0];
        NSString *strTempName = [[templateNode attributeForName:ATTRIBUTE_NAME] stringValue];
        NSString *tempName = [[NSString alloc] initWithString:strTempName];
        self.name = tempName;
        [tempName release];
        self.width = [[[templateNode attributeForName:ATTRIBUTE_W] stringValue] intValue];
        self.height = [[[templateNode attributeForName:ATTRIBUTE_H] stringValue] intValue];
        
        //读取所有TextBox
        NSArray *arrayNode = [templateNode elementsForName:NODE_TEXTBOX];
        NSMutableArray *arrayTxt = [[NSMutableArray alloc] initWithCapacity:[arrayNode count]];
        for (GDataXMLElement *elem in arrayNode)
        {
            TextBox *txtBox = [[TextBox alloc] init];
            //获取id
            NSString *strTxtID = [[elem attributeForName:ATTRIBUTE_ID] stringValue];
            txtBox.ID = [strTxtID intValue];
            //获取name
            NSString *strTxtName = [[elem attributeForName:ATTRIBUTE_NAME] stringValue];
            NSString *txtName = [[NSString alloc] initWithString:strTxtName];
            txtBox.name = txtName;
            [txtName release];
            //获取x
            NSString *strTxtX = [[elem attributeForName:ATTRIBUTE_X] stringValue];
            txtBox.x = [strTxtX intValue];
            //获取y
            NSString *strTxtY = [[elem attributeForName:ATTRIBUTE_Y] stringValue];
            txtBox.y = [strTxtY intValue];
            //获取w
            NSString *strTxtW = [[elem attributeForName:ATTRIBUTE_W] stringValue];
            txtBox.width = [strTxtW intValue];
            //获取h
            NSString *strTxtH = [[elem attributeForName:ATTRIBUTE_H] stringValue];
            txtBox.height = [strTxtH intValue];
            //获取align
            NSString *strTxtA = [[elem attributeForName:ATTRIBUTE_ALIGN] stringValue];
            if ([strTxtA isEqualToString:ATTRIBUTE_VALUE_L])
            {
                txtBox.align = TextAlignmentLeft;
            }
            else if ([strTxtA isEqualToString:ATTRIBUTE_VALUE_C])
            {
                txtBox.align = TextAlignmentCenter;
            }
            else if ([strTxtA isEqualToString:ATTRIBUTE_VALUE_R])
            {
                txtBox.align = TextAlignmentRight;
            }
            
            [arrayTxt addObject:txtBox];
            [txtBox release];
        }
        self.textBoxArray = arrayTxt;
        [arrayTxt release];
    }
    @catch (NSException *e)
    {
        if (doc != nil && [doc retainCount] > 0)
        {
            [doc release];
        }
        return NO;
    }
    
    [doc release];
    return YES;
}


- (NSMutableArray *)ParseStringsWithTextBox
{
    if (textBoxArray == nil || [textBoxArray count] == 0)
    {
        return nil;
    }
    
    //初始化模板上的所有块，并赋初值空格
    unichar *symbolBlocks = malloc(self.height*self.width*sizeof(unichar));
    void *free_me = symbolBlocks;
    for (int j=0; j<self.height; j++)
    {
        for (int i=0; i<self.width; i++)
        {
            symbolBlocks[j*self.width + i] = 32;
        }
    }
    
    for (TextBox *txt in textBoxArray)
    {
        int posValue = 0;  //字符串中遍历到单个字符的位置
        int valueLen = [txt.value length];
        int y2 = (txt.y+txt.height-1);
        int x2 = (txt.x+txt.width-1);
        
        //遍历所有的文本框，将其填充到symbolBlocks中
        for (int q=txt.y; q<=y2; q++)
        {
            int p = txt.x;
            switch (txt.align)
            {
                case TextAlignmentLeft:
                    p = txt.x;
                    break;
                case TextAlignmentRight:
                {
                    int subStrLen = [DotTemplate getCountOfString:[txt.value substringFromIndex:posValue]];
                    if (valueLen>=posValue+1 && subStrLen<txt.width)
                    {
                        p = txt.x + (txt.width-subStrLen);
                        if (p == txt.x+txt.width-1 && IS_CH_SYMBOL([txt.value characterAtIndex:posValue]))
                        {
                            p--;  //如果一行的最后一个字块存入中文，则需要从前一个位置开始存，因为汉字占两个位置
                            if (p<0) { continue; };
                        }
                    }
                }
                    break;
                case TextAlignmentCenter:
                {
                    int subStrLen = [DotTemplate getCountOfString:[txt.value substringFromIndex:posValue]];
                    if (valueLen>=posValue+1 && subStrLen<txt.width)
                    {
                        p = txt.x + (txt.width-subStrLen)/2;
                        if (p == txt.x+txt.width-1 && IS_CH_SYMBOL([txt.value characterAtIndex:posValue]))
                        {
                            p--;  //如果一行的最后一个字块存入中文，则需要从前一个位置开始存，因为汉字占两个位置
                            if (p<0) { continue; };
                        }
                    }
                }
                    break;
                default:
                    break;
            }
            
            for (; p<=x2; p++)
            {
                //如果value遍历到当前位置的字符串长度大于文本框的宽度，说明文本框存满了，退出循环。
                if (posValue+1 > valueLen) { break; }
                
                if (IS_CH_SYMBOL([txt.value characterAtIndex:posValue]))
                {
                    if (p == x2)
                    {
                        break;  //如果当前一个汉字要存入文本框中的其中一行的最后一个一个存储位置，则不存，直接存下一行中去
                    }
                    //正常存入一个汉字，设标志位为占用两个块的位置
                    symbolBlocks[q*self.width + p] = [txt.value characterAtIndex:posValue];
                    symbolBlocks[q*self.width + p+1] = FLAG_DISABLE;
                    p++;  //文本框中当前存储位置走到下一个
                }
                else
                {
                    symbolBlocks[q*self.width + p]=[txt.value characterAtIndex:posValue];
                }
                posValue++;
            }
            
        }
        
    }
    
    //将每一行的symbolBlocks块中的字符存入NSString，标记为FLAG_DISABLE的跳过不存。
    NSMutableArray *array = [[NSMutableArray alloc] initWithCapacity:self.height];
    unichar *blocks = malloc(self.width*self.height*sizeof(unichar));
    void *free_meNew = blocks;
    for (int m=0; m<=self.height-1; m++)
    {
        int posNewBlock = 0;
        int posOldBlock = 0;
        while(posOldBlock<self.width)
        {
            if (symbolBlocks[m*self.width + posOldBlock] != FLAG_DISABLE)
            {
                blocks[m*self.width + posNewBlock] = symbolBlocks[m*self.width + posOldBlock];
                posOldBlock++;  //将原字符块中的字一个个存入新区域，如果
                posNewBlock++;  //遇到FLAG_DISABLE占位的标记，则跳过。
            }
            else
            {
                blocks[m*self.width + posNewBlock] = '\n';
                posOldBlock++;
            }
        }

        //整理好了整个模板的一行数据，存入NSString中。
        NSString *string = [[NSString alloc] initWithCharacters:(const unichar*)(blocks+m*self.width) length:posNewBlock];
        [array insertObject:string atIndex:m];
        [string release];
    }

    free(free_me);
    free(free_meNew);
    return [array autorelease];
}





+ (int)getCountOfString:(NSString*)string
{
    int num=0;
    for (int i=0,cnt=[string length];i<cnt;i++)
    {
        unichar ch = [string characterAtIndex:i];
        if ((int)ch > 0x4e00 && ch < 0x9fff)
        {
            num+=2;
        }
        else
        {
            num++;
        }
    }
    return num;
}
@end
