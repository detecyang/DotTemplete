//
//  DotTemplate.m
//  DotTemplate
//
//  Created by yang on 13-03-08.
//  Copyright 2013 detecyang. All rights reserved.
//

#import "DotTemplate.h"


#define IS_CH_SYMBOL(chr) ((chr)>0x4e00 && (chr)<0x9fff)
#define FLAG_DISABLE 0xffff


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

- (int)getCountOfString:(NSString*)string
{
    int num=0;
    for (int i=0,cnt=[string length];i<cnt;i++)
    {
        unichar ch = [string characterAtIndex:i];
        if (ch > 0x4e00 && ch < 0x9fff)
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
            for (int p=txt.x; p<=x2; p++)
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
@end
