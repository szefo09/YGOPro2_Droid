LOCAL_PATH := $(call my-dir)/../../..

include $(CLEAR_VARS)
LOCAL_MODULE := sqlite3
LOCAL_SRC_FILES := ocgcore/sqlite3/sqlite3.c
include $(BUILD_SHARED_LIBRARY)

include $(CLEAR_VARS)
LOCAL_MODULE := ocgcore

ifndef NDEBUG
LOCAL_CFLAGS += -g -D_DEBUG -DLUA_USE_POSIX -DLUA_COMPAT_5_2
else
LOCAL_CFLAGS += -fexpensive-optimizations -O3 -DLUA_USE_POSIX -DLUA_COMPAT_5_2
endif

ifeq ($(TARGET_ARCH_ABI), x86)
LOCAL_CFLAGS += -fno-stack-protector
endif

ifeq ($(TARGET_ARCH_ABI), armeabi-v7a)
LOCAL_CFLAGS += -mno-unaligned-access
endif

LOCAL_C_INCLUDES := $(LOCAL_PATH)/ocgcore
LOCAL_C_INCLUDES += $(LOCAL_PATH)/ocgcore/lua
LOCAL_C_INCLUDES += $(LOCAL_PATH)/ocgcore/sqlite3

LOCAL_SRC_FILES := ocgcore/lua/lapi.c \
                   ocgcore/lua/lauxlib.c \
                   ocgcore/lua/lbaselib.c \
                   ocgcore/lua/lbitlib.c \
                   ocgcore/lua/lcode.c \
                   ocgcore/lua/lcorolib.c \
                   ocgcore/lua/lctype.c \
                   ocgcore/lua/ldblib.c \
                   ocgcore/lua/ldebug.c \
                   ocgcore/lua/ldo.c \
                   ocgcore/lua/ldump.c \
                   ocgcore/lua/lfunc.c \
                   ocgcore/lua/lgc.c \
                   ocgcore/lua/linit.c \
                   ocgcore/lua/liolib.c \
                   ocgcore/lua/llex.c \
                   ocgcore/lua/lmathlib.c \
                   ocgcore/lua/lmem.c \
                   ocgcore/lua/loadlib.c \
                   ocgcore/lua/lobject.c \
                   ocgcore/lua/lopcodes.c \
                   ocgcore/lua/loslib.c \
                   ocgcore/lua/lparser.c \
                   ocgcore/lua/lstate.c \
                   ocgcore/lua/lstring.c \
                   ocgcore/lua/lstrlib.c \
                   ocgcore/lua/ltable.c \
                   ocgcore/lua/ltablib.c \
                   ocgcore/lua/ltm.c \
                   ocgcore/lua/lundump.c \
                   ocgcore/lua/lutf8lib.c \
                   ocgcore/lua/lvm.c \
                   ocgcore/lua/lzio.c \
                   ocgcore/card.cpp \
                   ocgcore/duel.cpp \
                   ocgcore/effect.cpp \
                   ocgcore/field.cpp \
                   ocgcore/group.cpp \
                   ocgcore/interpreter.cpp \
                   ocgcore/libcard.cpp \
                   ocgcore/libdebug.cpp \
                   ocgcore/libduel.cpp \
                   ocgcore/libeffect.cpp \
                   ocgcore/libgroup.cpp \
                   ocgcore/mem.cpp \
                   ocgcore/ocgapi.cpp \
                   ocgcore/operations.cpp \
                   ocgcore/playerop.cpp \
                   ocgcore/processor.cpp \
                   ocgcore/scriptlib.cpp \

LOCAL_LDLIBS := -llog
include $(BUILD_SHARED_LIBRARY)

